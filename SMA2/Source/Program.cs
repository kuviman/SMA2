using System;
using VitPro;
using VitPro.Engine;

class Test : State {
}

class Program {
	static void Main(string[] args) {
		App.Title = "SMA2";
		App.Run(new Test());
	}
}