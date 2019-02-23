namespace Pixie.Cmd
{
    using PowerArgs;
    
    class RenderArgs
    {
        [ArgDefaultValue(200)]
        public int Width { get; set; }

        [ArgDefaultValue(100)]
        public int Height { get; set; }

        [ArgDefaultValue("out.ppm")]
        public string Out { get; set; }
    }
}