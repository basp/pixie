namespace Pixie
{
    using System.Collections.Generic;
    
    public class Pict
    {
        public float Width;
        public float Height;
        public IList<Pict> Children = new List<Pict>();
    }
}