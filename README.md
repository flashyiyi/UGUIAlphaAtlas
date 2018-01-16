# UGUIAlphaAtlas

这个玩意好歹有4个文件，而且也比较有价值，还是放到GIT上吧。

用Tools下的菜单命令`CreateAlphaAtlas`生成透明图集文件后，有两种方案应用：
- 把图集文件打入包内，然后用SplitImage替换所有的Image
- 调用`PackAlphaAltasToAssetBoundles`，生成替换了AlphaTexture属性的AssetBoundles，可以用原有的Image正常加载和显示

（现只支持IOS，因为只有这个平台有需求）

简介：<https://zhuanlan.zhihu.com/p/32674470>


`看Unity2018版本介绍，Unity似乎要支持IOS使用ETC2纹理了，这个方案估计要凉凉。`
