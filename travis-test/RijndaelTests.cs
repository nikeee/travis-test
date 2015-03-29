﻿using System;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace travis_test
{
    [TestFixture]
    public class RijndaelTests
    {
        private static readonly byte[] Key = { 0x8C, 0x35, 0x19, 0x2D, 0x96, 0x4D, 0xC3, 0x18, 0x2C, 0x6F, 0x84, 0xF3, 0x25, 0x22, 0x39, 0xEB, 0x4A, 0x32, 0x0D, 0x25, 0x00, 0x00, 0x00, 0x00 };
        private static readonly byte[] IV = { 0xA3, 0xD5, 0xA3, 0x3C, 0xB9, 0x5A, 0xC1, 0xF5, 0xCB, 0xDB, 0x1A, 0xD2, 0x5C, 0xB0, 0xA7, 0xAA };

        [Test]
        public void BlockSize()
        {

            RijndaelManaged rij = new RijndaelManaged
            {
                Padding = PaddingMode.None,
                FeedbackSize = 8,
                Mode = CipherMode.CFB,
                BlockSize = 128,
                KeySize = 128
            };

            Console.WriteLine("Key.Length: {0}", Key.Length);
            Console.WriteLine("IV.Length: {0}", IV.Length);

            byte[] cipherText = new byte[] { 0x8A, 0x69, 0x55, 0x7F, 0xB1, 0xDA, 0x75, 0x0E, 0xF4, 0xCE, 0x51, 0x65, 0x56, 0xE4, 0x80, 0x78, 0xDD, 0xEA, 0x9B, 0xC6, 0x3A, 0xBF, 0x9B, 0x0D, 0x1A, 0x44, 0x48, 0x01, 0x52, 0x2D, 0x30, 0xBA, 0xB6, 0x62, 0xBF, 0x1A, 0xAE, 0x73, 0x92, 0x86 };

            using (var dec = rij.CreateDecryptor(Key, IV))
            {
                var outputBuffer = new byte[cipherText.Length];
                /* exception thrown in next line (on mono?) */
                dec.TransformBlock(cipherText, 0, cipherText.Length, outputBuffer, 0);

                var link = Encoding.UTF8.GetString(outputBuffer);
                Assert.AreEqual("CCF: http://foo.example.org/rsdftest.bar", link);
            }
        }
    }
}
