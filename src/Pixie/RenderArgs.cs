namespace Pixie
{
    using PowerArgs;

    class RenderArgs
    {
        [ArgRequired, ArgPosition(1)]
        public string Asm { get; set; }

        [ArgRequired, ArgPosition(2)]
        public string Scene { get; set; }

        [ArgDefaultValue(200)]
        public int Width { get; set; }

        [ArgDefaultValue(100)]
        public int Height { get; set; }

        [ArgDefaultValue(4)]
        public int N { get; set; }

        [ArgDefaultValue("out.ppm")]
        public string Out { get; set; }
    }
}