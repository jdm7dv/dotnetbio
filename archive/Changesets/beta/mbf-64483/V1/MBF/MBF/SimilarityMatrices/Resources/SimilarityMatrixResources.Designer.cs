﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30128.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MBF.SimilarityMatrices.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SimilarityMatrixResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SimilarityMatrixResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MBF.SimilarityMatrices.Resources.SimilarityMatrixResources", typeof(SimilarityMatrixResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AmbiguousDNA
        /// A   T   G   C   S   W   R   Y   K   M   B   V   H   D   N
        /// 5  -4  -4  -4  -4   1   1  -4  -4   1  -4  -1  -1  -1   0
        ///-4   5  -4  -4  -4   1  -4   1   1  -4  -1  -4  -1  -1   0
        ///-4  -4   5  -4   1  -4   1  -4   1  -4  -1  -1  -4  -1   0
        ///-4  -4  -4   5   1  -4  -4   1  -4   1  -1  -1  -1  -4   0
        ///-4  -4   1   1	 5  -4  -2  -2  -2  -2  -1  -1  -3  -3   0
        /// 1   1  -4  -4  -4   5  -2  -2  -2  -2  -3  -3  -1  -1   0
        /// 1  -4   1  -4  -2  -2   5  -4  -2  -2  -3  -1  -3  -1   0
        ///-4   1  -4   1  -2  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AmbiguousDna {
            get {
                return ResourceManager.GetString("AmbiguousDna", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AmbiguousRNA
        /// A   U   G   C   S   W   R   Y   K   M   B   V   H   D   N
        /// 5  -4  -4  -4  -4   1   1  -4  -4   1  -4  -1  -1  -1   0
        ///-4   5  -4  -4  -4   1  -4   1   1  -4  -1  -4  -1  -1   0
        ///-4  -4   5  -4   1  -4   1  -4   1  -4  -1  -1  -4  -1   0
        ///-4  -4  -4   5   1  -4  -4   1  -4   1  -1  -1  -1  -4   0
        ///-4  -4   1   1	 5  -4  -2  -2  -2  -2  -1  -1  -3  -3   0
        /// 1   1  -4  -4  -4   5  -2  -2  -2  -2  -3  -3  -1  -1   0
        /// 1  -4   1  -4  -2  -2   5  -4  -2  -2  -3  -1  -3  -1   0
        ///-4   1  -4   1  -2  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string AmbiguousRna {
            get {
                return ResourceManager.GetString("AmbiguousRna", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BLOSUM45
        ///  A   R   N   D   C   Q   E   G   H   I   L   K   M   F   P   S   T   W   Y   V   B   J   Z   X   *
        ///  5  -2  -1  -2  -1  -1  -1   0  -2  -1  -1  -1  -1  -2  -1   1   0  -2  -2   0  -1  -1  -1  -1  -5
        /// -2   7   0  -1  -3   1   0  -2   0  -3  -2   3  -1  -2  -2  -1  -1  -2  -1  -2  -1  -3   1  -1  -5
        /// -1   0   6   2  -2   0   0   0   1  -2  -3   0  -2  -2  -2   1   0  -4  -2  -3   5  -3   0  -1  -5
        /// -2  -1   2   7  -3   0   2  -1   0  -4  -3   0  -3  -4  -1   0  -1  -4  -2  -3   6  -3   1  -1  - [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Blosum45 {
            get {
                return ResourceManager.GetString("Blosum45", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BLOSUM50
        ///  A   R   N   D   C   Q   E   G   H   I   L   K   M   F   P   S   T   W   Y   V   B   J   Z   X   *
        ///  5  -2  -1  -2  -1  -1  -1   0  -2  -1  -2  -1  -1  -3  -1   1   0  -3  -2   0  -2  -2  -1  -1  -5
        /// -2   7  -1  -2  -4   1   0  -3   0  -4  -3   3  -2  -3  -3  -1  -1  -3  -1  -3  -1  -3   0  -1  -5
        /// -1  -1   7   2  -2   0   0   0   1  -3  -4   0  -2  -4  -2   1   0  -4  -2  -3   5  -4   0  -1  -5
        /// -2  -2   2   8  -4   0   2  -1  -1  -4  -4  -1  -4  -5  -1   0  -1  -5  -3  -4   6  -4   1  -1  - [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Blosum50 {
            get {
                return ResourceManager.GetString("Blosum50", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BLOSUM62
        ///  A   R   N   D   C   Q   E   G   H   I   L   K   M   F   P   S   T   W   Y   V   B   J   Z   X   *
        ///  4  -1  -2  -2   0  -1  -1   0  -2  -1  -1  -1  -1  -2  -1   1   0  -3  -2   0  -2  -1  -1  -1  -4
        /// -1   5   0  -2  -3   1   0  -2   0  -3  -2   2  -1  -3  -2  -1  -1  -3  -2  -3  -1  -2   0  -1  -4
        /// -2   0   6   1  -3   0   0   0   1  -3  -3   0  -2  -3  -2   1   0  -4  -2  -3   4  -3   0  -1  -4
        /// -2  -2   1   6  -3   0   2  -1  -1  -3  -4  -1  -3  -3  -1   0  -1  -4  -3  -3   4  -3   1  -1  - [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Blosum62 {
            get {
                return ResourceManager.GetString("Blosum62", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BLOSUM80
        ///  A   R   N   D   C   Q   E   G   H   I   L   K   M   F   P   S   T   W   Y   V   B   J   Z   X   *
        ///  5  -2  -2  -2  -1  -1  -1   0  -2  -2  -2  -1  -1  -3  -1   1   0  -3  -2   0  -2  -2  -1  -1  -6
        /// -2   6  -1  -2  -4   1  -1  -3   0  -3  -3   2  -2  -4  -2  -1  -1  -4  -3  -3  -1  -3   0  -1  -6
        /// -2  -1   6   1  -3   0  -1  -1   0  -4  -4   0  -3  -4  -3   0   0  -4  -3  -4   5  -4   0  -1  -6
        /// -2  -2   1   6  -4  -1   1  -2  -2  -4  -5  -1  -4  -4  -2  -1  -1  -6  -4  -4   5  -5   1  -1  - [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Blosum80 {
            get {
                return ResourceManager.GetString("Blosum80", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BLOSUM90
        ///  A   R   N   D   C   Q   E   G   H   I   L   K   M   F   P   S   T   W   Y   V   B   J   Z   X   *
        ///  5  -2  -2  -3  -1  -1  -1   0  -2  -2  -2  -1  -2  -3  -1   1   0  -4  -3  -1  -2  -2  -1  -1  -6
        /// -2   6  -1  -3  -5   1  -1  -3   0  -4  -3   2  -2  -4  -3  -1  -2  -4  -3  -3  -2  -3   0  -1  -6
        /// -2  -1   7   1  -4   0  -1  -1   0  -4  -4   0  -3  -4  -3   0   0  -5  -3  -4   5  -4  -1  -1  -6
        /// -3  -3   1   7  -5  -1   1  -2  -2  -5  -5  -1  -4  -5  -3  -1  -2  -6  -4  -5   5  -5   1  -1  - [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Blosum90 {
            get {
                return ResourceManager.GetString("Blosum90", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DiagonalScoreMatrix
        ///  A   B   C   D   E   F   G   H   I   J   K   L   M   N   O   P   Q   R   S   T   U   V   W   X   Y   Z
        ///  3, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7 },
        /// -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7 },
        /// -7, -7,  3, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7 },
        /// -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, -7, - [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DiagonalScoreMatrix {
            get {
                return ResourceManager.GetString("DiagonalScoreMatrix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PAM250
        ///  A   R   N   D   C   Q   E   G   H   I   L   K   M   F   P   S   T   W   Y   V   B   J   Z   X   *
        ///  2  -2   0   0  -2   0   0   1  -1  -1  -2  -1  -1  -3   1   1   1  -6  -3   0   0  -1   0  -1  -8
        /// -2   6   0  -1  -4   1  -1  -3   2  -2  -3   3   0  -4   0   0  -1   2  -4  -2  -1  -3   0  -1  -8
        ///  0   0   2   2  -4   1   1   0   2  -2  -3   1  -2  -3   0   1   0  -4  -2  -2   2  -3   1  -1  -8
        ///  0  -1   2   4  -5   2   3   1   1  -2  -4   0  -3  -6  -1   0   0  -7  -4  -2   3  -3   3  -1  -8
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Pam250 {
            get {
                return ResourceManager.GetString("Pam250", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PAM30
        ///   A     R    N    D    C    Q    E    G    H    I    L    K    M    F    P    S    T    W    Y    V    B    J    Z    X   *
        ///   6    -7   -4   -3   -6   -4   -2   -2   -7   -5   -6   -7   -5   -8   -2    0   -1  -13   -8   -2   -3   -6   -3   -1  -17
        ///  -7     8   -6  -10   -8   -2   -9   -9   -2   -5   -8    0   -4   -9   -4   -3   -6   -2  -10   -8   -7   -7   -4   -1  -17
        ///  -4    -6    8    2  -11   -3   -2   -3    0   -5   -7   -1   -9   -9   -6    0   -2   -8   -4   -8    6   -6   -3   -1  -17 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Pam30 {
            get {
                return ResourceManager.GetString("Pam30", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PAM70
        ///  A     R    N    D    C    Q    E    G    H    I    L    K    M    F    P    S    T    W    Y    V    B    J    Z    X    *
        ///  5    -4   -2   -1   -4   -2   -1    0   -4   -2   -4   -4   -3   -6    0    1    1   -9   -5   -1   -1   -3   -1   -1  -11
        /// -4     8   -3   -6   -5    0   -5   -6    0   -3   -6    2   -2   -7   -2   -1   -4    0   -7   -5   -4   -5   -2   -1  -11
        /// -2    -3    6    3   -7   -1    0   -1    1   -3   -5    0   -5   -6   -3    1    0   -6   -3   -5    5   -4   -1   -1  -11
        ///  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Pam70 {
            get {
                return ResourceManager.GetString("Pam70", resourceCulture);
            }
        }
    }
}
