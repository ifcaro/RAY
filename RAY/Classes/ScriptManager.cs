using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace RAY
{
    internal class ScriptManager : IDisposable
    {
        private byte[] _newline = { 0x0d, 0x0a };

        public DataTable Strings;

        public void ReadScript()
        {
            Strings = new DataTable();
            Strings.Columns.Add("Source");
            Strings.Columns.Add("RawText", typeof(byte[]));
            Strings.Columns.Add("FinalText");

            using (FileStream fs = new FileStream(FileInfo.Rayus, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] line = ReadLine(br);

                    while (line != null)
                    {
                        Strings.Rows.Add(AddRow("RAYUS", line));

                        line = ReadLine(br);
                    }
                }
            }

            ReadExeStrings(Strings);
        }

        public bool SaveScript()
        {
            try
            {
                using (FileStream fs = new FileStream(FileInfo.Rayus, FileMode.Open))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        foreach (DataRow row in Strings.Rows)
                        {
                            if ((string)row[0] == "RAYUS")
                            {
                                byte[] bytes = ReplaceCharsReverse((string)row[2]);

                                bw.Write(bytes);
                                bw.Write(_newline);
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return SaveExeStrings(Strings);
        }

        public string ReplaceChars(byte[] value)
        {
            StringBuilder newValue = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                char replacement = RAY.FontManager.GetCharReplacement(value[i]);

                if (replacement != '\0')
                {
                    newValue.Append(replacement);
                }
                else
                {
                    newValue.Append(Encoding.ASCII.GetChars(new byte[] { value[i] }));
                }
            }

            return newValue.ToString();
        }

        public byte[] ReplaceCharsReverse(string value)
        {
            List<byte> newValue = new List<byte>();

            for (int i = 0; i < value.Length; i++)
            {
                int replacement = RAY.FontManager.GetCharReverseReplacement(value[i]);

                if (replacement != -1)
                {
                    newValue.Add((byte)replacement);
                }
                else
                {
                    newValue.Add(Encoding.ASCII.GetBytes(new char[] { value[i] })[0]);
                }
            }

            return newValue.ToArray();
        }

        private byte[] ReadLine(BinaryReader br)
        {
            List<byte> result = null;

            if (br.BaseStream.Position < br.BaseStream.Length)
            {
                result = new List<byte>();

                byte b = br.ReadByte();

                while (b != 0x0d && b != 0x0a)
                {
                    result.Add(b);

                    if (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        b = br.ReadByte();
                    }
                    else
                    {
                        break;
                    }
                }

                if (b == 0x0d)
                {
                    b = br.ReadByte();

                    if (b != 0x0a)
                    {
                        br.BaseStream.Position--;
                    }
                }

            }

            return result?.ToArray();
        }

        private DataRow AddRow(string source, byte[] buffer)
        {
            DataRow dataRow = Strings.NewRow();
            dataRow[0] = source;
            dataRow[1] = buffer;
            dataRow[2] = ReplaceChars(buffer);

            return dataRow;
        }

        private void ReadExeStrings(DataTable dataTable)
        {
            byte[] buffer = new byte[0x20];

            using (FileStream fs = new FileStream(FileInfo.Exe, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    fs.Position = 0xa3e54;

                    buffer = br.ReadBytes(buffer.Length);

                    Strings.Rows.Add(AddRow("EXE_GO", buffer));

                    fs.Position = 0xa3e74;

                    buffer = br.ReadBytes(buffer.Length);

                    Strings.Rows.Add(AddRow("EXE_PERFECT", buffer));

                    fs.Position = 0xa3e94;

                    buffer = br.ReadBytes(buffer.Length);

                    Strings.Rows.Add(AddRow("EXE_OK", buffer));
                }
            }
        }

        private bool SaveExeStrings(DataTable dataTable)
        {
            byte[] buffer = new byte[0x20];

            try
            {
                using (FileStream fs = new FileStream(FileInfo.Exe, FileMode.Open))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        foreach (DataRow row in Strings.Rows)
                        {
                            if ((string)row[0] == "EXE_GO")
                            {
                                fs.Position = 0xa3e54;

                                byte[] bytes = ReplaceCharsReverse((string)row[2]);

                                Array.Clear(buffer, 0, buffer.Length);
                                Array.Copy(bytes, 0, buffer, 0, Math.Min(bytes.Length, buffer.Length - 1));
                                buffer[buffer.Length - 1] = 0;

                                bw.Write(buffer, 0, buffer.Length);
                            }
                            else if ((string)row[0] == "EXE_PERFECT")
                            {
                                fs.Position = 0xa3e74;

                                byte[] bytes = ReplaceCharsReverse((string)row[2]);

                                Array.Clear(buffer, 0, buffer.Length);
                                Array.Copy(bytes, 0, buffer, 0, Math.Min(bytes.Length, buffer.Length - 1));
                                buffer[buffer.Length - 1] = 0;

                                bw.Write(buffer, 0, buffer.Length);
                            }
                            else if ((string)row[0] == "EXE_OK")
                            {
                                fs.Position = 0xa3e94;

                                byte[] bytes = ReplaceCharsReverse((string)row[2]);

                                Array.Clear(buffer, 0, buffer.Length);
                                Array.Copy(bytes, 0, buffer, 0, Math.Min(bytes.Length, buffer.Length - 1));
                                buffer[buffer.Length - 1] = 0;

                                bw.Write(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            if (Strings != null)
            {
                Strings.Dispose();
                Strings = null;
            }
        }
    }
}
