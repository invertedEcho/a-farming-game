using Godot;

// this script mainly handles spawning trees at the edge of our map.
// trees are spawned using MultiMeshInstance3D.
// it does so by choosing points mathematically along a ring -> see method: RandomEdgePoint
// then, 
public partial class Terrain : Node3D {
    [Export]
    MeshInstance3D terrainMesh;

    [Export]
    MultiMeshInstance3D treeMeshes;

    const int centerThreshold = 25;
    const int instanceCount = 150;

    public override void _Ready() {
        treeMeshes.Multimesh.InstanceCount = instanceCount;
        treeMeshes.Multimesh.Mesh = GD.Load<Mesh>("res://assets/models/nature/pine_tree_mesh.tres");

        Aabb boundingBox = terrainMesh.GetAabb();
        // our max radius, e.g. edge of terrain
        float maxRadius = boundingBox.Size.X;
        float minRadius = maxRadius - 2;

        for (int i = 0; i < instanceCount; i++) {
            Vector3 randomEdgePoint = RandomEdgePoint(minRadius, maxRadius);
            Vector3? snappedEdgePoint = SnapToTerrain(randomEdgePoint);
            if (snappedEdgePoint != null) {
                Transform3D transform = Transform3D.Identity;
                transform.Origin = (Vector3)snappedEdgePoint;
                transform.Origin.Y += 3f;
                treeMeshes.Multimesh.SetInstanceTransform(0, transform);
            }
        }
    }

    public static Vector3 RandomEdgePoint(float minRadius, float maxRadius) {
        float angle = GD.Randf() * Mathf.Tau;
        float radius = Mathf.Lerp(minRadius, maxRadius, GD.Randf());
        return new Vector3(
            Mathf.Cos(angle) * radius,
            100.0f, // We set Y to 100 to be able to raycast down later to get a valid spawn point for our trees
            Mathf.Sin(angle)
        );
    }

    public Vector3? SnapToTerrain(Vector3 position) {
        var space = GetWorld3D().DirectSpaceState;

        Vector3 start = position + Vector3.Up * 200.0f;
        Vector3 end = position + Vector3.Down * 200.0f;

        PhysicsRayQueryParameters3D queryParameters = new PhysicsRayQueryParameters3D();
        queryParameters.From = start;
        queryParameters.To = end;

        var rayCastResult = space.IntersectRay(queryParameters);

        Variant position2;
        if (rayCastResult.TryGetValue("position", out position2)) {
            GD.Print($"found top of position: {position2}");
            return (Vector3)position2;
        }
        return null;
    }
}
