﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This manager registers all modules so that they get used by the module based
 * marching cubes algorithm. Look for modules in its children.
 */
public class MarchingModuleManager : MonoBehaviour
{
    public class TransformedModule
    {
        public MarchingModule baseModule;
        public ModuleBasedMarchingCubes.Transform transform = new ModuleBasedMarchingCubes.Transform(0);
    }

    HashSet<TransformedModule>[] moduleSets; // at build time
    TransformedModule[][] moduleLut; // at sample time

    void RegisterModule(MarchingModule module)
    {
        module.Init();
        moduleSets[module.hash].Add(new TransformedModule { baseModule = module });

        if (module.allowRotationAroundVerticalAxis)
        {
            for (int i = 1; i < 4; ++i)
            {
                var transform = new ModuleBasedMarchingCubes.Transform(i);
                moduleSets[transform.TransformHash(module.hash)].Add(new TransformedModule { baseModule = module, transform = transform });
            }
        }
    }

    public void Prepare()
    {
        moduleLut = new TransformedModule[256][];
        for (int i = 0; i < 256; ++i)
        {
            if (moduleSets[i] == null || moduleSets[i].Count == 0) continue;
            moduleLut[i] = new TransformedModule[moduleSets[i].Count];
            int j = 0;
            foreach (var m in moduleSets[i])
            {
                moduleLut[i][j++] = m;
            }
        }
        moduleSets = null;
    }

    /**
     * Picks randomly a module valid for a given hash.
     * Requires Prepare() to be called before
     */
    public TransformedModule SampleModule(int hash)
    {
        Debug.Assert(hash >= 0 && hash < 256);
        if (moduleLut[hash] == null || moduleLut[hash].Length == 0) return null;
        int i = Random.Range(0, moduleLut[hash].Length);
        return moduleLut[hash][i];
    }

    void Start()
    {
        moduleSets = new HashSet<TransformedModule>[256];
        for (int i = 0; i < 256; ++i)
        {
            moduleSets[i] = new HashSet<TransformedModule>();
        }
        foreach (var module in GetComponentsInChildren<MarchingModule>())
        {
            RegisterModule(module);
        }
        Prepare();
    }
}
