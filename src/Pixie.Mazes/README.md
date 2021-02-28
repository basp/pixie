# Pixie.Mazes
```
void Main()
{
	var g = new CartesianGrid(16, 16);
	g.Sidewinder();
	g[0, 0].MatchSome(x =>
	{
		var d = new Distances(x);
		var m = new Maze(g, d);
		using (var bmp = m.Render())
		{
			bmp.Save(@"d:\temp\maze.png");
		}
	});
}
```