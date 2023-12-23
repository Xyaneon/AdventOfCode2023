using System.Collections.Immutable;

namespace Domain;

record Almanac(ImmutableList<long> SeedList,
               ImmutableDictionary<MapKind, Map> Maps);
