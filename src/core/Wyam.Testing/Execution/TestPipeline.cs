﻿using System;
using System.Collections.Generic;
using Wyam.Common;
using Wyam.Common.Execution;
using Wyam.Common.Modules;

namespace Wyam.Testing.Execution
{
    /// <summary>
    /// A pipeline with utility methods for easier testing.
    /// </summary>
    public class TestPipeline : IPipeline
    {
        public IModuleList ReadModules { get; } = new ModuleList();

        public IModuleList ProcessModules { get; } = new ModuleList();

        public IModuleList RenderModules { get; } = new ModuleList();

        public IModuleList WriteModules { get; } = new ModuleList();

        public HashSet<string> Dependencies { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public bool Isolated { get; set; }

        public bool AlwaysProcess { get; set; }

        public TestPipeline(params IModule[] processModules)
        {
            ProcessModules.AddRange(processModules);
        }

        public TestPipeline(IEnumerable<IModule> processModules)
        {
            ProcessModules.AddRange(processModules);
        }
    }
}