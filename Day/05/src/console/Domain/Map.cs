using System.Collections.Immutable;

namespace Domain;

record Map(ImmutableList<MapRange> Ranges);
