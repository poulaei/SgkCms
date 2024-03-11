using CmsKitDemo.Entities;

namespace CmsKitDemo
{
    public class BoxConsts
    {
        
        public const string EntityType = "Box";
        public static int SectionMaxLength { get; set; } = 25;
        public static int TitleMaxLength { get; set; } = 50;
        public static int ActionMaxLength { get; set; } = 25;
        public static int ActionUrlMaxLength { get; set; } = 100;
        public static int SummaryMaxLength { get; set; } = 100;
        public static int DescriptionMaxLength { get; set; } = 512;


    }
    public class BoxItemConsts
    {

        public const string EntityType = "BoxItem";
        public static int TitleMaxLength { get; set; } = 50;
        public static int ActionMaxLength { get; set; } = 25;
        public static int ActionUrlMaxLength { get; set; } = 100;
        public static int SummaryMaxLength { get; set; } = 100;    
        public static int DescriptionMaxLength { get; set; } = 1024;
        public static int IconMaxLength { get; set; } = 25;
    }
}
