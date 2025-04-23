using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Rarity { Common, Rare, Epic, Legendary }

public abstract class Artifact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PowerLevel { get; set; }
    public Rarity Rarity { get; set; }

    public abstract string Serialize();
}