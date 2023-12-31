﻿using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UCloth
{
    /// <summary>
    /// Transforms vertices from world-space simulation to local-space for rendering.
    /// </summary>
    [BurstCompile]
    internal struct TransformVerticesToLocalJob : IJobParallelFor
    {
        [WriteOnly]
        public NativeArray<float3> localSpaceVertices;

        [ReadOnly]
        public NativeArray<float3> worldSpaceVertices;


        [ReadOnly]
        public NativeArray<int> renderToSimIndexLookup;

        public float4x4 worldToLocal;

        public void Execute(int index)
        {
            int targetVertex = renderToSimIndexLookup[index];

            // Translate into local space
            localSpaceVertices[index] = math.transform(worldToLocal, worldSpaceVertices[targetVertex]);

        }
    }
}
