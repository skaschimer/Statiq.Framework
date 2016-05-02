﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Wyam.Common.IO;
using Wyam.Configuration.Preprocessing;

namespace Wyam.Configuration.NuGet
{
    internal class NuGetConfigDirective : IDirective
    {
        private readonly PackageInstaller _packageInstaller;

        public NuGetConfigDirective(PackageInstaller packageInstaller)
        {
            _packageInstaller = packageInstaller;
        }

        public void Process(string value)
        {
            DirectoryPath packagesPath = null;
            bool useLocal = false;
            bool updatePackages = false;

            // Parse the directive value
            IEnumerable<string> arguments = ArgumentSplitter.Split(value);
            System.CommandLine.ArgumentSyntax parsed = System.CommandLine.ArgumentSyntax.Parse(arguments, syntax =>
            {
                if (syntax.DefineOption("use-local-packages", ref useLocal, "Toggles the use of a local NuGet packages folder.").IsSpecified)
                {
                    _packageInstaller.UseLocal = useLocal;
                }
                if (syntax.DefineOption("update-packages", ref updatePackages, "Check the NuGet server for more recent versions of each package and update them if applicable.").IsSpecified)
                {
                    _packageInstaller.UpdatePackages = updatePackages;
                }
                if (syntax.DefineParameter("packages-path", ref packagesPath, DirectoryPath.FromString, "The packages path to use (only if use-local is true).").IsSpecified)
                {
                    _packageInstaller.PackagesPath = packagesPath;
                }
            });
            if (parsed.HasErrors)
            {
                throw new Exception(parsed.GetHelpText());
            }
        }
    }
}
