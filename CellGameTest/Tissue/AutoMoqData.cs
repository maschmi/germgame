﻿using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace CellGameTest.Tissue
{
    public class AutoMoqData : AutoDataAttribute
    {
        public AutoMoqData()
            : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
            
        }
    }
}