using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.DoInDirectory
{
    public static class MyCakeExtension
    {
        [CakeMethodAlias]
        public static void DoInDirectory(this ICakeContext context, DirectoryPath directory, Action task)
        {
            var original = context.Environment.WorkingDirectory;
            try
            {
                context.Environment.WorkingDirectory = directory.MakeAbsolute(context.Environment);
                task();
            }
            finally
            {
                context.Environment.WorkingDirectory = original;
            }
        }
    }
}
