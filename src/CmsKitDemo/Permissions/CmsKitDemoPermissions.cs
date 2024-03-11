namespace CmsKitDemo.Permissions
{ //Added by poolaei @1402/12/18
    public static class CmsKitDemoPermissions
    {
        public const string GroupName = "CmsKitDemo";

        public static class GalleryImage
        {
            public const string Root = GroupName + ".GalleryImage";

            public const string Management = Root + ".Management";
            public const string Create = Root + ".Create";
            public const string Update = Root + ".Update";
            public const string Delete = Root + ".Delete";
        }
        //Added by poolaei @1402/12/21==========================
        public static class Box
        {
            public const string Root = GroupName + ".Box";

            public const string Management = Root + ".Management";
            public const string Create = Root + ".Create";
            public const string Update = Root + ".Update";
            public const string Delete = Root + ".Delete";
        }
        public static class BoxItem
        {
            public const string Root = GroupName + ".BoxItem";

            public const string Management = Root + ".Management";
            public const string Create = Root + ".Create";
            public const string Update = Root + ".Update";
            public const string Delete = Root + ".Delete";
        }
        //===================================================
    }
}
