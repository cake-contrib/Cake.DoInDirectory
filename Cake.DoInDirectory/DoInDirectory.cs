using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.DoInDirectory
{
    /// <summary>
    /// Contains functionality for changing the current directory for an operation
    /// </summary>
    [CakeAliasCategory("DoInDirectory")]
    public static class DoInDirectoryAliases
    {
        /// <summary>
        /// Changes the current working directory before an <see cref="Action"/> and returns to the previous one in the end
        /// </summary>
        /// <example>
        /// <para>Use the #addin preprocessor directive</para>
        /// <code>
        /// <![CDATA[
        /// #addin nuget:?package=Cake.DoInDirectory
        /// ]]>
        /// </code>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("SomeTask").Does(() =>
        /// {
        ///     DoInDirectory("./SomeDir", () =>
        ///     {
        ///         // Do awesome stuff
        ///     });
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="directory">The desired working directory for the action.</param>
        /// <param name="task">The action to be executed.</param>
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
