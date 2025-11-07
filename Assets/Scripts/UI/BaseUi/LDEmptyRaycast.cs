namespace UnityEngine.UI
{
    public class LDEmptyRaycast : MaskableGraphic
    {
        protected LDEmptyRaycast()
        {
            useLegacyMeshGeneration = false;
        }
        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            toFill.Clear();

        }
    }
}
