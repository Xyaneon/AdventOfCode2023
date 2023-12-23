using System.Collections.Immutable;

namespace Domain;

record Almanac(ImmutableList<int> SeedList,
               ImmutableDictionary<MapKind, Map> Maps);
