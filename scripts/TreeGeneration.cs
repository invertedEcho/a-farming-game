using Godot;

public partial class TreeGeneration : MultiMeshInstance3D {
    public override void _Ready() {
        Multimesh.InstanceCount = 5;
        Multimesh.Mesh = GD.Load<Mesh>("res://assets/models/nature/pine_tree_mesh.tres");

        for (int i = 0; i < 5; i++) {
            Transform3D transform = Transform3D.Identity;
            transform.Origin = new Vector3(3f + i, 3f, 3f);

            Multimesh.SetInstanceTransform(i, transform);
        }
    }
}