﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApi.Common.Logic.Exceptions {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Exceptions {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Exceptions() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApi.Common.Logic.Exceptions.Exceptions", typeof(Exceptions).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Uno de los argumentos proporcionados no es válido..
        /// </summary>
        public static string ArgumentException {
            get {
                return ResourceManager.GetString("ArgumentException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Argumento null no válido para uno de los métodos..
        /// </summary>
        public static string ArgumentNullException {
            get {
                return ResourceManager.GetString("ArgumentNullException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Valor de un argumento fuera del intervalo permitido..
        /// </summary>
        public static string ArgumentOutOfRangeException {
            get {
                return ResourceManager.GetString("ArgumentOutOfRangeException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Directorio no encontrado..
        /// </summary>
        public static string DirectoryNotFoundException {
            get {
                return ResourceManager.GetString("DirectoryNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Archivo no encontrado..
        /// </summary>
        public static string FileNotFoundException {
            get {
                return ResourceManager.GetString("FileNotFoundException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Formato no válido o incorrecto..
        /// </summary>
        public static string FormatException {
            get {
                return ResourceManager.GetString("FormatException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Error de E/S..
        /// </summary>
        public static string IOException {
            get {
                return ResourceManager.GetString("IOException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Memoria insuficiente para continuar con la ejecución..
        /// </summary>
        public static string OutOfMemoryException {
            get {
                return ResourceManager.GetString("OutOfMemoryException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Desbordamiento  por conversión incorrecta..
        /// </summary>
        public static string OverflowException {
            get {
                return ResourceManager.GetString("OverflowException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Una característica no se ha ejecutado en una plataforma concreta..
        /// </summary>
        public static string PlatformNotSupportedException {
            get {
                return ResourceManager.GetString("PlatformNotSupportedException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Se ha intentado invocar un destino incorrecto..
        /// </summary>
        public static string TargetException {
            get {
                return ResourceManager.GetString("TargetException", resourceCulture);
            }
        }
    }
}
