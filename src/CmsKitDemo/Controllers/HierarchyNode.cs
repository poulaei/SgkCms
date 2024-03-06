namespace CmsKitDemo
{
    /// <summary>
    /// Hierarchy node class which contains a nested collection of hierarchy nodes
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// 
    [Serializable]
    public class HierarchyNode<T> where T : class
    {
        public T Entity { get; set; }
        public IEnumerable<HierarchyNode<T>>? Children { get; set; }
        public int Depth { get; set; }
        public T? Parent { get; set; }
    }
}
