# pixie
This is a set of experimental classes for computer graphics. The only remotely useful thing is the turtle graphics component which is described below.

## turtle graphics
This is a very barebones implementation of turtle graphics for .NET. It was created mostly so I could play around with turtle graphics in LINQPad and not having to worry about lots of external dependencies. It includes no drawing routines, just the vectoring logic. In order to actually draw stuff it relies on adapter interfaces (with `System.Drawing` in mind).

### instantiating a turtle
The example below assumes we have a `GraphicsAdapter` class around that implements the `IGraphics` interface required by the turtle.

```
using System.Drawing;

using (var bmp = new Bitmap(300, 300))
using (var gfx = Graphics.FromImage(bmp))
{
    var adapter = new GraphicsAdapter(gfx);
    var turtle = new Turtle(adapter);
}
```

The turtle always starts in the `0, 0` position which is at the top-left corner of the canvas.

### moving around
The turtle knows only `Turn(degrees)` and `Forward(units)` commands. The `Turn` command assumes a clockwise direction. If you want to turn counter-clockwise, specify a negative amount of degrees (i.e `-45.0f`). Moving around is in *units* which roughly correspond to pixels in a bitmap.

### drawing
Once you issue the `PenDown` command the turtle starts drawing with the current pen settings. If you issue the `PenUp` command the turtle stops drawing and you can move it around without messing up the canvas.

### remembering locations
The turtle has exceptional memory but unfortionately it can only remember things in order. That means you can only ask him about the last location he visited before he remembers the next one.

```
var turtle = new Turtle(adapter);
turtle
    .Push()         // remember location 0
    .Forward(10)
    .Push()         // remember location 1
    .Forward(10)
    .Pop()          // turtle is at location 1
    .Forward(10)
    .Pop();         // turtle is at location 0
```

