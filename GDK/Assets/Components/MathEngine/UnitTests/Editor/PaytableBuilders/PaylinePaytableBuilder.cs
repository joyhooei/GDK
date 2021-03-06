﻿using System;
using System.Collections.Generic;
using GDK.MathEngine;

public class PaylinePaytableBuilder : PaytableBuilder
{
	public override ReelGroup BuildBGReelGroup ()
	{
		ReelGroup reels = new ReelGroup ();

		ReelStrip reel1 = new ReelStrip ();
		reel1.AddSymbol (new Symbol (0, "AA"));
		reel1.AddSymbol (new Symbol (1, "BB"));
		reel1.AddSymbol (new Symbol (2, "CC"));
		reel1.AddSymbol (new Symbol (3, "DD"));
		reel1.AddSymbol (new Symbol (4, "EE"));
		reel1.AddSymbol (new Symbol (5, "FF"));
		reel1.AddSymbol (new Symbol (6, "GG"));

		ReelStrip reel2 = new ReelStrip ();
		reel2.AddSymbol (new Symbol (0, "AA"));
		reel2.AddSymbol (new Symbol (1, "BB"));
		reel2.AddSymbol (new Symbol (2, "CC"));
		reel2.AddSymbol (new Symbol (3, "DD"));
		reel2.AddSymbol (new Symbol (4, "EE"));
		reel2.AddSymbol (new Symbol (5, "FF"));
		reel2.AddSymbol (new Symbol (6, "GG"));

		ReelStrip reel3 = new ReelStrip ();
		reel3.AddSymbol (new Symbol (0, "AA"));
		reel3.AddSymbol (new Symbol (1, "BB"));
		reel3.AddSymbol (new Symbol (2, "CC"));
		reel3.AddSymbol (new Symbol (3, "DD"));
		reel3.AddSymbol (new Symbol (4, "EE"));
		reel3.AddSymbol (new Symbol (5, "FF"));
		reel3.AddSymbol (new Symbol (6, "GG"));

		reels.AddReel (reel1);
		reels.AddReel (reel2);
		reels.AddReel (reel3);

		return reels;
	}

	public override PaylineGroup BuildPaylineGroup ()
	{
		PaylineGroup paylines = new PaylineGroup ();

		Payline payline1 = new Payline ();
		payline1.AddPaylineCoord (new PaylineCoord { ReelIndex = 0, Offset = 0 });
		payline1.AddPaylineCoord (new PaylineCoord { ReelIndex = 1, Offset = 0 });
		payline1.AddPaylineCoord (new PaylineCoord { ReelIndex = 2, Offset = 0 });

		Payline payline2 = new Payline ();
		payline2.AddPaylineCoord (new PaylineCoord { ReelIndex = 0, Offset = 1 });
		payline2.AddPaylineCoord (new PaylineCoord { ReelIndex = 1, Offset = 1 });
		payline2.AddPaylineCoord (new PaylineCoord { ReelIndex = 2, Offset = 1 });

		Payline payline3 = new Payline ();
		payline3.AddPaylineCoord (new PaylineCoord { ReelIndex = 0, Offset = 2 });
		payline3.AddPaylineCoord (new PaylineCoord { ReelIndex = 1, Offset = 2 });
		payline3.AddPaylineCoord (new PaylineCoord { ReelIndex = 2, Offset = 2 });

		paylines.AddPayline (payline1);
		paylines.AddPayline (payline2);
		paylines.AddPayline (payline3);

		return paylines;
	}

	public override PayComboGroup BuildPayComboGroup ()
	{
		PayComboGroup payCombos = new PayComboGroup (new SymbolComparer());

		// TODO: This is where we might want to add the triggers.
		payCombos.AddPayCombo (new PayCombo (new Symbol (0, "AA"), 2, 10));
		payCombos.AddPayCombo (new PayCombo (new Symbol (0, "AA"), 3, 100));
		payCombos.AddPayCombo (new PayCombo (new Symbol (1, "BB"), 3, 50));
		payCombos.AddPayCombo (new PayCombo (new Symbol (2, "CC"), 3, 20));

		return payCombos;
	}
}