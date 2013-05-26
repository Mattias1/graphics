﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace amulware.Graphics
{
    /// <summary>
    /// Immutable class representing a font.
    /// </summary>
    public sealed class Font
    {
        /// <summary>
        /// The offset of the 0-char in the texture in uv coordinates.
        /// </summary>
        public Vector2 UVSymbolOffset { get; private set; }
        /// <summary>
        /// Syze of the symbol in the texture in uv coordinates.
        /// </summary>
        public Vector2 UVSymbolSize { get; private set; }
        /// <summary>
        /// Default size of a symbol when drawing text with this font.
        /// </summary>
        public Vector2 SymbolSize { get; private set; }

        /// <summary>
        /// Whether this font is monospaced.
        /// </summary>
        public bool Monospaced { get; private set; }

        private float[] letterWidths;


        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="uvSymbolOffset">The offset of the 0-char in the texture in uv coordinates.</param>
        /// <param name="uvSymbolSize">Size of a symbol in the texture in uv coordinates.</param>
        /// <param name="symbolSize">Default size of a symbol when drawing text.</param>
        /// <param name="letterWidths">Relative widths of the symbols. Null for monospaced font.</param>
        private Font(Vector2 uvSymbolOffset, Vector2 uvSymbolSize,
            Vector2 symbolSize, float[] letterWidths = null)
        {
            this.UVSymbolOffset = uvSymbolOffset;
            this.UVSymbolSize = uvSymbolSize;
            this.SymbolSize = symbolSize;
            if (letterWidths != null)
            {
                this.letterWidths = new float[256];
                int max = Math.Max(letterWidths.Length, 256);
                Array.Copy(letterWidths, this.letterWidths, max);
            }
            this.Monospaced = this.letterWidths == null;
        }

        /// <summary>
        /// Relative width of a symbol. Throws an exception if the font is monospaced.
        /// </summary>
        /// <param name="c">ASCII code of symbol. Range: [0, 255]</param>
        /// <returns>Relative width</returns>
        public float LetterWidth(int c)
        {
            return this.letterWidths[c];
        }

        /// <summary>
        /// Font builder, used to create <see cref="Font"/>s.
        /// </summary>
        public sealed class Builder
        {
            /// <summary>
            /// The offset of the 0-char in the texture in uv coordinates.
            /// </summary>
            public Vector2 UVSymbolOffset { get; set; }
            /// <summary>
            /// Syze of the symbol in the texture in uv coordinates.
            /// </summary>
            public Vector2 UVSymbolSize { get; set; }
            /// <summary>
            /// Default size of a symbol when drawing text with built fonts.
            /// </summary>
            public Vector2 SymbolSize { get; set; }

            /// <summary>
            /// Default size of a symbol when drawing text with built fonts.
            /// </summary>
            public float[] LetterWidths { get; set; }

            /// <summary>
            /// Initialises a new font builder with default settings.
            /// </summary>
            public Builder()
            {
                this.UVSymbolSize = new Vector2(1f / 16f, 1f / 16f);
                this.UVSymbolOffset = Vector2.Zero;
                this.SymbolSize = Vector2.One;
                this.LetterWidths = null;
            }

            /// <summary>
            /// Builds a <see cref="Font"/> from the current settings.
            /// </summary>
            /// <returns>A <see cref="Font"/>.</returns>
            public Font Build()
            {
                return new Font(this.UVSymbolOffset, this.UVSymbolSize,
                    this.SymbolSize, this.LetterWidths);
            }
        }
    }
}
