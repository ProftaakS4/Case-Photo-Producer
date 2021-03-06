﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Sockets;

namespace PhotoshopWebsite.WebSocket
{
    public class WebSocketSingleton
    {
        // create instance of its owne class
        private static WebSocketSingleton _singleton = null;

        // create private constructor
        private WebSocketSingleton()
        {
            // instance
        }

        /// <summary>
        /// Get the instance of the socket singleton
        /// </summary>
        /// <returns></returns>
        public static WebSocketSingleton GetSingleton()
        {
            if (_singleton == null)
            {
                _singleton = new WebSocketSingleton();
                return _singleton;
            }
            return _singleton;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param> contains the photoID and the quantity
        public bool sendData(string photoID)
        {
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("192.168.27.123"), 4343);

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(serverAddress);

                // Sending
                int toSendLen = System.Text.Encoding.ASCII.GetByteCount(photoID);
                byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(photoID);
                byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
                clientSocket.Send(toSendLenBytes);
                clientSocket.Send(toSendBytes);
                return true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("No Socket Connection.");
            }
            finally
            {
                clientSocket.Close();
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param> contains the photoID and the quantity
        public void sendCropData(string photoID)
        {
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("192.168.27.123"), 4343);

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(serverAddress);

                // Sending
                int toSendLen = System.Text.Encoding.ASCII.GetByteCount(photoID);
                byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(photoID);
                byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
                clientSocket.Send(toSendLenBytes);
                clientSocket.Send(toSendBytes);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("No Socket Connection.");
            }
            finally
            {
                clientSocket.Close();
            }
        }
    }
}
