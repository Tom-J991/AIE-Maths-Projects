using System;

namespace MathClasses
{
    public struct Colour
    {
        // this will store four bytes representing RGBA in the most to last significant bytes
        public UInt32 colour;

        public Colour(byte r, byte g, byte b, byte a)
        {
            colour = 0x00000000; // RR GG BB AA
            red = r;
            green = g;
            blue = b;
            alpha = a;
        }

        public byte red
        {
            get
            {
                byte result;
                // Bit mask the colour variable so that only the byte representing the colour (0xRRGGBBAA) will be modified then shift it to the least significant bit.
                // Type cast it to a byte so that it is only read as one byte (byte/char) instead of four bytes (int/uint)
                int shift = 8 * 3; // 8 bytes * offset;
                result = (byte)((colour & 0xFF000000) >> shift);
                return result;
            }
            set
            {
                // Bit mask the colour variable so that the first byte is reset back to 0s then shift the new value into the right place and add it into the colour variable.
                // Type cast it back to a UInt32 since the bit manipulation results in a long type?
                int shift = 8 * 3;
                int mask = ~(0xFF << shift);
                colour = (UInt32)(colour & mask | (value << shift));
            }
        }
        public byte green
        {
            get
            {
                byte result;
                int shift = 8 * 2;
                result = (byte)((colour & 0x00FF0000) >> shift);
                return result;
            }
            set
            {
                int shift = 8 * 2;
                int mask = ~(0xFF << shift);
                colour = (UInt32)(colour & mask | (value << shift));
            }
        }
        public byte blue
        {
            get
            {
                byte result;
                int shift = 8 * 1;
                result = (byte)((colour & 0x0000FF00) >> shift);
                return result;
            }
            set
            {
                int shift = 8 * 1;
                int mask = ~(0xFF << shift);
                colour = (UInt32)(colour & mask | (value << shift));
            }
        }
        public byte alpha
        {
            get
            {
                byte result;
                int shift = 8 * 0;
                result = (byte)((colour & 0x000000FF) >> shift);
                return result;
            }
            set
            {
                int shift = 8 * 0;
                int mask = ~(0xFF << shift);
                colour = (UInt32)(colour & mask | (value << shift));
            }
        }
    }
}
