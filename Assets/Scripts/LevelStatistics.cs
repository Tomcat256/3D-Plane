using System.Text;

public class LevelStatistics {
	public uint ChestsCollected = 0;
	public uint AstroidsDestroyed = 0;
	public uint MissilesLeft = 0;

	public uint ScoresPerCrate {
		get	{ return 1;}
	}

	public uint ScoresPerAstroid{
		get { return 10;}
	}

	public uint ScoresPerMissileLeft{
		get { return 1;}
	}

	public uint Total()
	{
		return ChestsCollected * ScoresPerCrate + AstroidsDestroyed * ScoresPerAstroid + MissilesLeft * ScoresPerMissileLeft; 
	}

	override public string ToString()
	{
		StringBuilder sb = new StringBuilder ();
		sb.Append ("Asteroids Destroyed:").Append(" ").Append(AstroidsDestroyed).Append("x").Append(ScoresPerAstroid).Append(" = ").Append(AstroidsDestroyed * ScoresPerAstroid).Append("\n");
		sb.Append ("Chest Collected:").Append(" ").Append(ChestsCollected).Append("x").Append(ScoresPerCrate).Append(" = ").Append(ChestsCollected * ScoresPerCrate).Append("\n");
		sb.Append ("Missiles Left:").Append(" ").Append(MissilesLeft).Append("x").Append(ScoresPerMissileLeft).Append(" = ").Append(MissilesLeft * ScoresPerMissileLeft).Append("\n");
		sb.AppendLine ();
		sb.Append ("Total score: ").Append (Total ());
		return sb.ToString ();
	}
}
