﻿/***********************************************************************************************
 COPYRIGHT 2008 Vijeth D

 This file is part of NeuronDotNet.
 (Project Website : http://neurondotnet.freehostia.com)

 NeuronDotNet is a free software. You can redistribute it and/or modify it under the terms of
 the GNU General Public License as published by the Free Software Foundation, either version 3
 of the License, or (at your option) any later version.

 NeuronDotNet is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 See the GNU General Public License for more details.

 You should have received a copy of the GNU General Public License along with NeuronDotNet.
 If not, see <http://www.gnu.org/licenses/>.

***********************************************************************************************/

using System;
using System.Runtime.Serialization;
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.SOM;

namespace NeuronDotNet.Core.Initializers
{
    /// <summary>
    /// 一个<见cref =“IInitializer”/>使用常量函数
    /// </summary>
    [Serializable]
    public class ConstantFunction : IInitializer
    {
        private readonly double constant;

        /// <summary>
        /// 获取初始化常量
        /// </summary>
        /// <value>
        /// 初始化每个参数（偏置值和权重）的常数
        /// </value>
        public double Constant
        {
            get { return constant; }
        }

        /// <summary>
        /// 创建新的常数函数
        /// </summary>
        /// <param name="constant">
        /// 常数使用
        /// </param>
        public ConstantFunction(double constant)
        {
            this.constant = constant;
        }

        /// <summary>
        /// 反序列化构造函数
        /// </summary>
        /// <param name="info">
        /// 序列化信息反序列化和获取数据
        /// </param>
        /// <param name="context">
        /// 要使用的序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> info </ c>是<c> null </ c>
        /// </exception>
        public ConstantFunction(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");
            constant = info.GetDouble("constant");
        }

        /// <summary>
        /// 用序列化初始化程序所需的数据填充序列化信息
        /// </summary>
        /// <param name="info">
        /// 用于填充数据的序列化信息
        /// </param>
        /// <param name="context">
        /// 要使用的序列化上下文
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c> info </ c>是<c> null </ c>
        /// </exception>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Helper.ValidateNotNull(info, "info");
            info.AddValue("constant", constant);
        }

        /// <summary>
        /// Initializes bias values of activation neurons in the activation layer.
        /// </summary>
        /// <param name="activationLayer">
        /// The activation layer to initialize
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <c>activationLayer</c> is <c>null</c>
        /// </exception>
        public void Initialize(ActivationLayer activationLayer)
        {
            Helper.ValidateNotNull(activationLayer, "layer");
            foreach (ActivationNeuron neuron in activationLayer.Neurons)
            {
                neuron.bias = constant;
            }
        }

        /// <summary>
        /// 初始化反向传播连接器中的所有反向传播突触的权重。
        /// </summary>
        /// <param name="connector">
        /// 反向传播连接器初始化。
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c>连接器</ c>为<c> null </ c>
        /// </exception>
        public void Initialize(BackpropagationConnector connector)
        {
            Helper.ValidateNotNull(connector, "connector");
            foreach (BackpropagationSynapse synapse in connector.Synapses)
            {
                synapse.Weight = constant;
            }
        }

        /// <summary>
        /// 初始化Kohonen连接器中所有空间突触的权重。
        /// </summary>
        /// <param name="connector">
        /// Kohonen连接器初始化。
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// 如果<c>连接器</ c>为<c> null </ c>
        /// </exception>
        public void Initialize(KohonenConnector connector)
        {
            Helper.ValidateNotNull(connector, "connector");
            foreach (KohonenSynapse synapse in connector.Synapses)
            {
                synapse.Weight = constant;
            }
        }
    }
}