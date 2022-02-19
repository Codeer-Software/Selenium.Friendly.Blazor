namespace BlazorApp
{
    public class Sample2
    {
        public int Value { get; set; }
    }
    public class Sample1
    {
        public static string Test() => "aaa";
        public static int Test2() => 100;
        public static int Test3 { get; set; } = 1000;
        public static Sample2 Sample2 => new Sample2();
    }
}
