using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkAnalysis.Core.Models;

namespace SocialNetworkAnalysis.Core.Algorithms
{
    public class CommunityDetection : ICommunityDetection
    {
        public CommunityDetectionResult Execute(SocialGraph graph)
        {
            var allNodes = graph.GetUsers()?.ToList();
            if (allNodes == null || allNodes.Count == 0)
            {
                return new CommunityDetectionResult();
            }

            Dictionary<int, int> nodeDegrees = new Dictionary<int, int>();
            foreach (int node in allNodes)
            {
                var friends = graph.GetFriends(node);
                int degree = friends?.Count() ?? 0;

                nodeDegrees[node] = degree;
            }

            var sortedNodes = nodeDegrees
                .OrderBy(x => x.Value)
                .Select(x => x.Key)
                .ToList();

            Dictionary<int, int> nodeToCommunity = new();
            foreach (int node in sortedNodes)
            {
                nodeToCommunity[node] = node;
            }

            RunLouvainPass(graph, sortedNodes, nodeToCommunity, strictMaxWeight: false);

            var localCommunities = ExtractCommunitiesFromMap(nodeToCommunity);

            RunLouvainPass(graph, sortedNodes, nodeToCommunity, strictMaxWeight: true);

            var globalCommunities = ExtractCommunitiesFromMap(nodeToCommunity);

            return new CommunityDetectionResult
            {
                LocalCommunities = localCommunities,
                LocalCommunitiesCount = localCommunities.Count,

                GlobalCommunities = globalCommunities,
                GlobalCommunitiesCount = globalCommunities.Count
            };
        }

        private void RunLouvainPass(SocialGraph graph, List<int> nodes, Dictionary<int, int> nodeToCommunity, bool strictMaxWeight)
        {
            bool someNodeMoved = true;
            int maxIterations = 20;
            int iteration = 0;

            while (someNodeMoved && iteration < maxIterations)
            {
                someNodeMoved = false;
                iteration++;

                foreach (int currentNode in nodes)
                {
                    int currentCommunity = nodeToCommunity[currentNode];
                    var friends = graph.GetFriends(currentNode)?.ToList();
                    if (friends == null || friends.Count == 0) continue;

                    Dictionary<int, int> communityWeights = new();

                    communityWeights[currentCommunity] = 0;
                    foreach (int friend in friends)
                    {
                        int friendCommunity = nodeToCommunity[friend];

                        if (communityWeights.ContainsKey(friendCommunity))
                        {
                            communityWeights[friendCommunity]++;
                        }
                        else
                        {
                            communityWeights[friendCommunity] = 1;
                        }
                    }

                    int bestCommunity = currentCommunity;
                    int maxWeight = communityWeights[currentCommunity];

                    foreach (var x in communityWeights)
                    {
                        bool canMove = false;

                        if (strictMaxWeight == true)
                        {
                            if (x.Value > maxWeight)
                            {
                                canMove = true;
                            }
                        }
                        else 
                        {
                            if (x.Value >= maxWeight)
                            {
                                canMove = true;
                            }
                        }

                        if (canMove)
                        {
                            maxWeight = x.Value;
                            bestCommunity = x.Key;
                        }
                    }

                    if (bestCommunity != currentCommunity)
                    {
                        nodeToCommunity[currentNode] = bestCommunity;
                        someNodeMoved = true;
                    }
                }
            }
        }

        private List<List<int>> ExtractCommunitiesFromMap(Dictionary<int, int> nodeToCommunity)
        {
            Dictionary<int, List<int>> communityGroups = new();
            foreach (var x in nodeToCommunity)
            {
                int node = x.Key;
                int communityId = x.Value;

                if (!communityGroups.ContainsKey(communityId))
                {
                    communityGroups[communityId] = new List<int>();
                }
                communityGroups[communityId].Add(node);
            }
            return communityGroups.Values.ToList();
        }
    }
}
