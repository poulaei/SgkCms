
export interface HierarchyNode<T> {
  entity: T;
  children: HierarchyNode;
  depth: number;
  parent: T;
}
