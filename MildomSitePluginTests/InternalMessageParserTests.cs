﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MildomSitePlugin.InternalMessage;
namespace MildomSitePluginTests
{
    [TestFixture]
    public class InternalMessageParserTests
    {
        [TestCase(
            new uint[] { 1231715354, 3090269522, 1629681486, 467467442, 2986036621, 3945298736, 3710645329, 1927512237, 218809194, 420036435, 3865082474, 2543133722, 947043872, 4249841658, 3556308547, 1686532916, 2162007120, 1668524858, 66218501, 1436371109, 6057611 },
            new uint[] { 1785344561, 1768502323, 1701131829, 1646535488, 556410466, 626944361, 7237156 },
            new uint[] { 1835213435, 574235236, 1801680236, 1718175609, 2020557428, 1635021626, 1433630068, 1952539760, 573317733, 1635017060, 578501154, 1735357040, 1936942450, 875706914, 1948396595, 1701278305, 825893492, 741355568, 577005858, 2105356602, 80 }
            )]
        public void Test(uint[] t, uint[] e, uint[] expected)
        {
            var actual = InternalMessageParser.sub_c(t, e);
            CollectionAssert.AreEqual(actual, expected);
        }
        [TestCase(
            "GnxqSVLFMbhO9yJhsvzcG41N+7EweyjrUfQr3a2A43JqwwoNUz8JGWp6YOYaJJWXIL5yOPpvT/1D9vjTNHOGZFCc3YA6q3NjBWryA6VInVWLblwA",
            "12jj34ii56ee@#$bb&*!ii^%$nn",
            "{\"cmd\":\"luckyGiftBox:statusUpdate\",\"data\":{\"progress\":243,\"target\":1000,\"id\":1}}"
            )]
        [TestCase("6lYY7z1G30IB9mx+rRY2WmCmmmns0QVtJC4c2RZ5rw9grfHt/2xY67oa0KVDiCrTvHQHBnQzlqA5pULa2jD6IJM+U1jq58yMYc4RdwQGGNgRSt8k",
            "12jj34ii56ee@#$bb&*!ii^%$nn",
            "{\"cmd\":\"luckyGiftBox:statusUpdate\",\"data\":{\"progress\":701,\"target\":1000,\"id\":1}}"
            )]
        public void DecryptToStringTest(string t, string e, string expected)
        {
            var actual = InternalMessageParser.decryptToString(t, e);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(
            new byte[] { 0, 4, 0, 0, 0, 0, 1, 40, 181, 54, 57, 184, 219, 30, 87, 142, 125, 202, 41, 54, 108, 53, 198, 205, 9, 215, 163, 4, 68, 49, 233, 67, 119, 119, 121, 238, 77, 79, 58, 182, 161, 102, 11, 79, 168, 253, 132, 226, 145, 193, 8, 77, 241, 184, 173, 228, 175, 237, 226, 220, 10, 144, 162, 62, 45, 157, 227, 104, 118, 253, 106, 40, 180, 224, 208, 58, 85, 235, 107, 71, 2, 172, 90, 107, 99, 39, 130, 171, 94, 127, 114, 64, 195, 239, 39, 171, 253, 16, 214, 234, 169, 243, 159, 176, 143, 111, 190, 164, 155, 97, 211, 163, 253, 46, 255, 116, 51, 208, 122, 133, 40, 230, 32, 19, 231, 1, 2, 235, 55, 225, 142, 76, 165, 212, 95, 32, 169, 108, 171, 48, 18, 40, 153, 69, 55, 138, 138, 136, 22, 200, 69, 50, 24, 159, 75, 86, 166, 243, 202, 214, 197, 173, 249, 16, 4, 187, 156, 47, 63, 156, 176, 123, 81, 136, 60, 237, 232, 208, 57, 87, 229, 12, 155, 43, 137, 35, 150, 86, 87, 89, 248, 222, 38, 197, 213, 86, 152, 238, 251, 110, 60, 83, 23, 85, 199, 134, 38, 111, 28, 75, 240, 177, 201, 34, 216, 110, 186, 60, 236, 164, 47, 67, 249, 61, 124, 69, 33, 94, 39, 116, 167, 206, 147, 254, 47, 39, 197, 131, 170, 235, 194, 226, 11, 138, 0, 146, 158, 249, 33, 184, 69, 43, 61, 72, 146, 15, 214, 85, 248, 48, 44, 102, 225, 168, 220, 247, 139, 9, 154, 202, 69, 93, 8, 60, 165, 40, 52, 54, 112, 237, 128, 45, 201, 246, 90, 97, 84, 64, 132, 79, 96, 42, 26, 240, 113, 115, 215, 225, 183, 127, 161, 106, 249, 184 },
            "{\"area\": 1000, \"avatarDecortaion\": 0, \"cmd\": \"onAdd\", \"enterroomEffect\": 0, \"isFirstTopup\": null, \"level\": 1, \"loveCountSum\": 0, \"medals\": null, \"nobleLevel\": 0, \"reqId\": 0, \"roomId\": 12615345, \"rst\": 0, \"type\": 3, \"userCount\": 1064, \"userId\": 0, \"userImg\": null, \"userName\": \"guest393134\"}")]
        [TestCase(
            new byte[] { 0, 4, 1, 1, 0, 0, 3, 48, 80, 241, 109, 206, 176, 90, 190, 209, 25, 65, 195, 169, 26, 156, 57, 46, 100, 217, 202, 62, 195, 244, 211, 179, 105, 23, 130, 35, 64, 111, 128, 27, 65, 145, 163, 134, 72, 129, 220, 31, 188, 97, 238, 197, 89, 143, 131, 44, 58, 46, 85, 248, 91, 190, 104, 36, 17, 226, 212, 213, 198, 96, 135, 217, 31, 49, 243, 253, 67, 79, 179, 60, 106, 33, 41, 198, 92, 130, 9, 202, 253, 220, 139, 165, 193, 131, 121, 219, 1, 230, 36, 213, 58, 74, 169, 162, 158, 232, 144, 178, 105, 163, 92, 20, 186, 4, 53, 29, 150, 241, 130, 247, 67, 187, 250, 197, 251, 159, 55, 143, 15, 7, 202, 112, 26, 76, 208, 11, 192, 196, 159, 14, 27, 81, 151, 191, 98, 133, 244, 1, 128, 230, 142, 145, 175, 95, 222, 105, 86, 147, 218, 36, 210, 42, 181, 3, 216, 107, 159, 230, 143, 101, 146, 177, 218, 213, 165, 86, 97, 205, 244, 106, 143, 70, 92, 220, 11, 192, 109, 31, 23, 233, 142, 201, 86, 159, 28, 93, 9, 135, 42, 53, 210, 185, 161, 136, 192, 23, 157, 49, 92, 177, 2, 160, 93, 132, 164, 25, 224, 244, 236, 229, 197, 199, 28, 111, 62, 140, 254, 168, 223, 63, 189, 65, 52, 239, 74, 92, 111, 51, 139, 4, 4, 134, 191, 247, 111, 17, 90, 218, 100, 18, 36, 8, 47, 61, 207, 82, 219, 213, 212, 26, 35, 119, 27, 130, 216, 241, 50, 86, 0, 231, 218, 187, 212, 3, 46, 164, 147, 160, 50, 229, 11, 116, 14, 135, 205, 236, 236, 146, 26, 240, 121, 152, 67, 9, 192, 27, 131, 161, 83, 189, 109, 46, 146, 185, 77, 181, 33, 191, 226, 39, 45, 141, 50, 217, 74, 239, 143, 92, 136, 75, 238, 7, 196, 20, 218, 97, 229, 1, 201, 229, 237, 154, 49, 165, 230, 73, 190, 148, 199, 194, 42, 171, 186, 82, 33, 166, 122, 101, 129, 219, 96, 178, 179, 18, 2, 95, 219, 128, 235, 106, 145, 110, 54, 137, 34, 35, 87, 127, 207, 109, 184, 193, 251, 105, 206, 188, 137, 23, 80, 95, 24, 253, 53, 197, 89, 227, 213, 3, 41, 196, 145, 225, 208, 22, 30, 228, 67, 69, 107, 229, 219, 216, 178, 192, 205, 231, 80, 71, 169, 216, 115, 195, 189, 34, 43, 65, 138, 118, 156, 200, 180, 229, 160, 90, 168, 59, 9, 98, 251, 239, 9, 72, 51, 121, 16, 213, 14, 217, 226, 195, 74, 121, 26, 204, 100, 171, 163, 195, 226, 84, 8, 97, 160, 201, 154, 152, 43, 76, 37, 136, 53, 58, 180, 161, 125, 13, 233, 2, 19, 24, 64, 18, 174, 195, 217, 165, 17, 29, 17, 203, 66, 230, 201, 97, 182, 106, 88, 158, 173, 141, 226, 162, 148, 81, 184, 2, 239, 186, 60, 201, 0, 148, 87, 79, 48, 111, 42, 8, 18, 38, 120, 206, 149, 10, 253, 212, 27, 116, 62, 21, 95, 62, 191, 204, 13, 133, 43, 147, 233, 85, 229, 175, 174, 57, 168, 185, 161, 11, 209, 164, 12, 69, 250, 179, 1, 89, 39, 108, 251, 205, 21, 87, 126, 202, 78, 251, 20, 82, 12, 66, 196, 158, 36, 153, 88, 179, 182, 253, 198, 44, 29, 65, 223, 113, 91, 127, 241, 255, 238, 203, 252, 43, 163, 22, 40, 161, 72, 10, 165, 186, 232, 214, 4, 99, 223, 239, 86, 221, 189, 6, 144, 48, 238, 37, 50, 242, 248, 71, 53, 251, 26, 122, 158, 204, 100, 44, 188, 28, 41, 74, 205, 28, 180, 18, 86, 134, 5, 72, 71, 171, 41, 21, 117, 97, 4, 123, 116, 62, 149, 15, 200, 164, 29, 12, 5, 19, 160, 175, 252, 56, 142, 163, 191, 222, 139, 70, 217, 95, 146, 229, 8, 115, 33, 214, 7, 38, 156, 244, 69, 251, 240, 178, 68, 167, 44, 217, 78, 37, 253, 85, 249, 132, 158, 30, 149, 167, 178, 71, 234, 32, 151, 32, 160, 65, 64, 217, 9, 34, 208, 66, 47, 7, 19, 27, 112, 166, 233, 158, 44, 147, 144, 29, 179, 132, 109, 2, 195, 179, 42, 7, 82, 69, 169, 201, 196, 24, 17, 222, 98, 186, 102, 245, 164, 27, 169, 3, 125, 20, 241, 255, 224, 1, 172, 152, 94, 251, 148, 216, 239, 245, 149, 115, 71, 6, 27, 66, 121, 101, 105, 107, 47, 194, 136, 42, 128, 141, 119, 182, 60, 120, 71, 130, 130, 188, 82, 181, 104, 114, 49, 216, 225, 196, 180, 210, 188, 191, 240, 185, 13, 214, 66, 210, 79, 214, 103, 139, 114, 136, 223, 215, 27, 144, 241, 52, 142, 238, 124, 107, 254, 114, 90, 214, 79, 213, 116, 17, 214, 55, 82, 72, 254, 140, 238, 218 },
            "{\"userId\":0,\"level\":1,\"userName\":\"guest429839\",\"gareaArgsObj\":{\"source\":\"homepage\",\"sub_source\":\"face_up_live\"},\"guestId\":\"pc-gp-c5f6f156-12ab-4f7a-9f7a-13f103f2799a\",\"nonopara\":\"fr=web`sfr=pc`devi=Windows 10 64-bit`la=ja`gid=pc-gp-c5f6f156-12ab-4f7a-9f7a-13f103f2799a`na=Japan`loc=Japan|Kanagawa`clu=aws_japan`wh=1920*1080`rtm=2022-01-30T14:46:10.215Z`ua=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36`click_time=2022-01-30 23:46:07.788`pcv=v3.8.5`source=homepage`sub_source=face_up_live`room_id=13284111`live_id=13284111-c7r96n4irkrcmvq9bal0`live_status=live`live_content_type=face_up_live\",\"roomId\":13284111,\"reqId\":1,\"cmd\":\"enterRoom\",\"reConnect\":0,\"nobleLevel\":0,\"avatarDecortaion\":0,\"enterroomEffect\":0,\"nobleClose\":0,\"nobleSeatClose\":0}")]
        [TestCase(
            new byte[] { 0, 4, 0, 0, 0, 0, 0, 124, 162, 11, 83, 105, 58, 217, 204, 60, 178, 223, 210, 235, 155, 17, 46, 226, 216, 87, 103, 115, 26, 138, 11, 106, 52, 165, 255, 72, 113, 23, 249, 144, 210, 76, 75, 144, 220, 240, 241, 162, 11, 24, 234, 66, 196, 246, 25, 141, 146, 189, 14, 45, 233, 68, 10, 190, 217, 37, 89, 99, 147, 173, 193, 133, 115, 9, 0, 44, 138, 208, 109, 64, 120, 184, 10, 42, 75, 140, 74, 67, 235, 95, 80, 181, 51, 33, 95, 114, 54, 231, 47, 79, 138, 125, 87, 207, 20, 197, 101, 189, 193, 58, 68, 68, 161, 31, 203, 230, 37, 117, 7, 230, 165, 46, 72, 34, 73, 21, 95, 188, 211, 247, 58, 171 },
            "{\"admin\": 0, \"cmd\": \"enterRoom\", \"fobiddenGlobal\": 0, \"forbidden\": 0, \"reqId\": 1, \"rst\": 0, \"type\": 2, \"userCount\": 12}"
            )]
        [TestCase(
            new byte[] { 0, 4, 0, 0, 0, 0, 2, 156, 83, 62, 233, 20, 61, 68, 134, 181, 30, 8, 10, 222, 194, 243, 241, 164, 111, 164, 100, 197, 9, 179, 57, 211, 112, 53, 165, 13, 83, 90, 227, 95, 91, 142, 26, 200, 239, 36, 47, 154, 0, 208, 49, 81, 157, 233, 217, 81, 19, 34, 234, 68, 117, 188, 61, 147, 33, 99, 37, 3, 110, 100, 169, 213, 236, 227, 159, 27, 10, 66, 4, 175, 122, 225, 67, 157, 94, 54, 107, 205, 114, 89, 209, 42, 203, 228, 166, 83, 60, 11, 154, 10, 27, 9, 146, 255, 167, 72, 223, 77, 89, 181, 171, 26, 16, 166, 138, 165, 194, 84, 201, 124, 189, 178, 74, 194, 83, 31, 152, 68, 219, 126, 199, 146, 87, 79, 137, 103, 249, 163, 250, 67, 100, 111, 131, 250, 105, 58, 4, 89, 74, 123, 233, 41, 67, 58, 40, 190, 76, 190, 171, 147, 190, 185, 245, 226, 219, 224, 10, 82, 176, 212, 88, 100, 179, 59, 73, 154, 89, 51, 215, 167, 190, 147, 93, 240, 12, 241, 17, 207, 187, 155, 51, 250, 63, 123, 167, 168, 82, 6, 149, 234, 223, 230, 251, 148, 142, 24, 223, 31, 207, 131, 157, 140, 57, 228, 60, 93, 232, 114, 164, 184, 218, 178, 149, 77, 83, 3, 249, 245, 101, 189, 17, 222, 177, 192, 215, 27, 93, 95, 107, 210, 139, 160, 95, 20, 23, 48, 94, 202, 52, 156, 9, 69, 253, 134, 52, 132, 205, 30, 63, 104, 17, 2, 81, 164, 135, 198, 81, 199, 83, 122, 77, 219, 143, 175, 114, 83, 98, 26, 6, 102, 108, 55, 74, 27, 152, 30, 250, 247, 6, 213, 6, 163, 198, 15, 109, 13, 139, 57, 29, 197, 245, 82, 230, 177, 84, 39, 105, 6, 244, 203, 113, 47, 186, 254, 206, 121, 40, 213, 79, 53, 126, 170, 30, 60, 110, 233, 133, 208, 152, 169, 12, 182, 233, 156, 248, 199, 212, 250, 250, 75, 241, 156, 187, 175, 24, 203, 86, 177, 166, 36, 130, 12, 49, 78, 111, 220, 62, 101, 116, 243, 138, 172, 217, 201, 251, 206, 129, 21, 209, 176, 192, 2, 100, 205, 27, 142, 180, 24, 180, 38, 97, 180, 21, 203, 34, 238, 239, 103, 112, 112, 189, 159, 43, 127, 130, 147, 13, 179, 84, 167, 166, 203, 185, 218, 245, 188, 6, 62, 158, 139, 74, 27, 89, 105, 228, 170, 47, 57, 33, 11, 204, 180, 242, 104, 30, 32, 177, 238, 21, 212, 70, 206, 237, 199, 97, 75, 145, 217, 212, 248, 115, 202, 225, 136, 158, 73, 10, 90, 37, 157, 89, 159, 27, 195, 26, 21, 90, 126, 67, 53, 95, 144, 225, 1, 99, 211, 118, 179, 1, 51, 96, 133, 60, 71, 72, 227, 77, 73, 85, 114, 37, 50, 119, 226, 171, 138, 153, 101, 209, 182, 123, 198, 10, 207, 90, 72, 127, 101, 113, 71, 206, 121, 64, 143, 227, 50, 2, 58, 118, 77, 170, 174, 185, 130, 136, 59, 225, 8, 117, 201, 13, 31, 33, 202, 18, 193, 186, 51, 152, 113, 76, 64, 193, 13, 254, 217, 18, 186, 190, 74, 86, 126, 194, 125, 210, 229, 63, 235, 82, 20, 141, 46, 251, 74, 170, 193, 29, 113, 64, 69, 138, 83, 36, 132, 73, 232, 64, 203, 244, 186, 80, 231, 51, 21, 252, 172, 194, 24, 72, 151, 82, 145, 151, 88, 47, 98, 92, 231, 102, 225, 226, 21, 163, 65, 151, 180, 201, 121, 2, 180, 206, 192, 130, 26, 63, 60, 169, 53, 78, 185, 59, 47, 193, 91, 168, 67, 34, 253, 245, 175, 142, 254, 216, 190, 108, 173, 124, 193, 27, 134, 238, 57, 80, 10, 76, 193, 83, 212, 199, 104, 205, 49, 109, 70, 151, 164, 239, 17, 124, 20, 250, 98, 153, 127, 254, 15, 10, 107, 17, 125, 128, 181, 59, 11, 50, 199, 185, 126, 32, 138, 44, 236, 89, 127, 28, 243 },
            "{\"area\": 2000, \"cmd\": \"onChat\", \"fansBgPic\": null, \"fansGroupType\": 0, \"fansLevel\": null, \"fansName\": null, \"gareaReturnObj\": {\"badges\": [{\"level\": 20, \"privilege_id\": 1, \"privilege_name\": \"🍓軍\", \"privilege_type\": 1}], \"user_pendant\": null}, \"isFirstTopup\": 0, \"level\": 89, \"medals\": null, \"msg\": \"1週間お疲れ様でした\", \"msgId\": \"1643558443054_12142777_9876\", \"reqId\": 0, \"roomAdmin\": 0, \"roomId\": 13284111, \"time\": \"1643558443054\", \"toId\": 13284111, \"toName\": \"みさん(苺)\", \"type\": 3, \"userId\": 12142777, \"userImg\": \"https://isscdn.mildom.tv/download/file/jp/mildom/nnphotos/12142777/8a77bcd67a1919ce3cb6b8812d0b2a47.png\", \"userName\": \"D-oka\"}"
            )]
        public void DecryptMessageTest(byte[] t, string expected)
        {
            var actual = InternalMessageParser.DecryptMessage(t);
            Assert.AreEqual(expected, actual);
        }
        //[TestCase("{\"cmd\":\"enterRoom\",\"msg_data\":{\"user_id\":0,\"loginname\":\"guest429839\",\"noble_id\":0,\"noble_level\":0,\"avatar_decortation_id\":0,\"re_connect\":1,\"nonopara\":\"fr=web`sfr=pc`devi=Windows 10 64-bit`la=ja`gid=pc-gp-c5f6f156-12ab-4f7a-9f7a-13f103f2799a`na=Japan`loc=Japan|Kanagawa`clu=aws_japan`wh=1920*1080`rtm=2022-01-31T15:27:09.515Z`ua=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36`click_time=2022-01-30 23:46:07.788`pcv=v3.8.5\"},\"req_id\":5}",
        //    "12jj34ii56ee@#$bb&*!ii^%$nn",
        //    new byte[] { 0, 3, 1, 1, 0, 0, 2, 168, 112, 107, 117, 83, 109, 70, 105, 80, 98, 52, 88, 98, 56, 98, 113, 106, 98, 75, 90, 110, 47, 73, 102, 67, 99, 122, 86, 102, 54, 85, 104, 99, 115, 54, 55, 111, 97, 71, 121, 57, 118, 89, 82, 72, 118, 54, 53, 88, 55, 118, 52, 119, 114, 106, 67, 73, 51, 101, 86, 80, 48, 80, 98, 81, 101, 68, 82, 122, 111, 48, 122, 106, 75, 66, 65, 65, 98, 74, 90, 110, 101, 110, 85, 71, 113, 50, 103, 51, 52, 87, 78, 113, 117, 108, 43, 87, 79, 101, 113, 75, 121, 119, 48, 112, 52, 80, 97, 108, 100, 87, 119, 102, 89, 82, 113, 76, 78, 47, 111, 104, 111, 109, 71, 48, 102, 107, 78, 113, 73, 71, 114, 89, 71, 89, 97, 43, 119, 84, 100, 47, 100, 80, 97, 110, 83, 121, 107, 103, 71, 65, 100, 107, 79, 43, 104, 117, 107, 50, 72, 119, 88, 72, 117, 65, 104, 87, 97, 105, 74, 72, 103, 116, 112, 87, 115, 54, 78, 103, 110, 99, 68, 85, 73, 108, 67, 97, 55, 66, 116, 106, 115, 53, 43, 52, 119, 55, 81, 53, 52, 65, 75, 50, 88, 47, 104, 84, 105, 43, 118, 43, 81, 121, 85, 65, 99, 50, 52, 119, 102, 102, 56, 73, 48, 76, 43, 75, 103, 117, 83, 83, 102, 98, 57, 119, 65, 121, 57, 52, 73, 79, 79, 53, 81, 100, 83, 100, 72, 74, 102, 122, 82, 53, 53, 67, 104, 120, 100, 109, 106, 102, 108, 53, 77, 73, 118, 48, 119, 84, 70, 107, 117, 99, 103, 74, 77, 84, 85, 53, 53, 89, 118, 57, 114, 117, 99, 72, 119, 55, 106, 54, 67, 79, 106, 43, 122, 56, 107, 103, 89, 99, 70, 54, 78, 52, 87, 110, 104, 55, 79, 68, 69, 112, 82, 102, 43, 67, 68, 113, 116, 120, 73, 115, 49, 82, 122, 85, 71, 99, 65, 80, 53, 107, 80, 86, 110, 70, 71, 119, 119, 105, 110, 116, 119, 90, 116, 52, 67, 88, 86, 119, 111, 107, 99, 50, 121, 75, 71, 78, 106, 70, 65, 85, 101, 114, 116, 81, 70, 47, 86, 79, 55, 98, 43, 87, 115, 53, 102, 106, 114, 84, 74, 69, 71, 115, 80, 83, 113, 105, 56, 122, 56, 80, 76, 71, 109, 110, 71, 65, 120, 84, 54, 119, 84, 117, 110, 114, 82, 105, 84, 48, 108, 56, 115, 111, 89, 116, 47, 57, 81, 86, 87, 52, 66, 121, 107, 76, 81, 55, 43, 70, 82, 50, 86, 114, 121, 48, 115, 52, 110, 70, 115, 113, 107, 78, 66, 114, 113, 121, 118, 107, 116, 71, 120, 115, 103, 51, 51, 77, 85, 80, 114, 83, 89, 74, 66, 109, 67, 76, 73, 89, 75, 69, 67, 78, 100, 75, 75, 102, 57, 100, 118, 116, 73, 49, 55, 111, 87, 87, 81, 114, 65, 102, 70, 66, 87, 81, 101, 114, 56, 114, 90, 82, 76, 79, 112, 114, 88, 51, 75, 82, 77, 111, 110, 97, 119, 75, 104, 67, 51, 107, 72, 121, 65, 67, 77, 67, 98, 77, 104, 102, 52, 118, 53, 66, 104, 104, 98, 78, 80, 57, 116, 104, 114, 54, 76, 99, 108, 99, 75, 109, 51, 115, 109, 70, 53, 84, 117, 66, 99, 110, 76, 52, 74, 108, 98, 54, 101, 98, 75, 121, 122, 105, 71, 57, 103, 79, 89, 87, 79, 100, 100, 78, 115, 114, 88, 115, 52, 69, 79, 65, 56, 57, 116, 75, 86, 105, 98, 71, 81, 102, 70, 48, 115, 82, 110, 49, 115, 48, 65, 103, 117, 121, 68, 120, 78, 118, 112, 78, 113, 51, 56, 65, 43, 86, 114, 108, 103, 85, 114, 112, 83, 104, 83, 70, 88, 50, 71, 120, 98, 56, 119, 78, 114, 73, 117, 108, 50, 116, 79, 65, 56, 50, 77, 117, 71, 105, 72, 68, 82, 80, 86, 47, 69, 83, 78, 86, 103, 104, 100, 49, 53, 104, 52, 43, 100, 70, 73, 65, 61, 61 }
        //    )]
        //public void EncryptDecryptMessageWithBase64Test(string t, string e, byte[] expected)
        //{
        //    var actual = InternalMessageParser.EncryptMessageWithBase64(t);
        //    CollectionAssert.AreEqual(expected, actual);
        //}

        [TestCase(
            "{\"cmd\":\"enterRoom\",\"msg_data\":{\"user_id\":0,\"loginname\":\"guest429839\",\"noble_id\":0,\"noble_level\":0,\"avatar_decortation_id\":0,\"re_connect\":1,\"nonopara\":\"fr=web`sfr=pc`devi=Windows 10 64-bit`la=ja`gid=pc-gp-c5f6f156-12ab-4f7a-9f7a-13f103f2799a`na=Japan`loc=Japan|Kanagawa`clu=aws_japan`wh=1920*1080`rtm=2022-01-31T15:27:09.515Z`ua=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.99 Safari/537.36`click_time=2022-01-30 23:46:07.788`pcv=v3.8.5\"},\"req_id\":5}",
            "12jj34ii56ee@#$bb&*!ii^%$nn",
            "pkuSmFiPb4Xb8bqjbKZn/IfCczVf6Uhcs67oaGy9vYRHv65X7v4wrjCI3eVP0PbQeDRzo0zjKBAAbJZnenUGq2g34WNqul+WOeqKyw0p4PaldWwfYRqLN/ohomG0fkNqIGrYGYa+wTd/dPanSykgGAdkO+huk2HwXHuAhWaiJHgtpWs6NgncDUIlCa7Btjs5+4w7Q54AK2X/hTi+v+QyUAc24wff8I0L+KguSSfb9wAy94IOO5QdSdHJfzR55Chxdmjfl5MIv0wTFkucgJMTU55Yv9rucHw7j6COj+z8kgYcF6N4Wnh7ODEpRf+CDqtxIs1RzUGcAP5kPVnFGwwintwZt4CXVwokc2yKGNjFAUertQF/VO7b+Ws5fjrTJEGsPSqi8z8PLGmnGAxT6wTunrRiT0l8soYt/9QVW4BykLQ7+FR2Vry0s4nFsqkNBrqyvktGxsg33MUPrSYJBmCLIYKECNdKKf9dvtI17oWWQrAfFBWQer8rZRLOprX3KRMonawKhC3kHyACMCbMhf4v5BhhbNP9thr6LclcKm3smF5TuBcnL4Jlb6ebKyziG9gOYWOddNsrXs4EOA89tKVibGQfF0sRn1s0AguyDxNvpNq38A+VrlgUrpShSFX2Gxb8wNrIul2tOA82MuGiHDRPV/ESNVghd15h4+dFIA=="
            )]
        public void EncryptToStringTest(string t, string e, string expected)
        {
            var actual = InternalMessageParser.encryptToString(t, e);
            Assert.AreEqual(expected, actual);
        }
        [TestCase("abc")]
        public void EncryptDecryptMessageTest(string str)
        {
            var encrypted = InternalMessageParser.EnctyptMessage(str);
            var decrypted = InternalMessageParser.DecryptMessage(encrypted);
            
            Assert.AreEqual(str, decrypted);
        }
        [TestCase("abc")]
        public void EncryptDecryptMessageWithBase64Test(string str)
        {
            var encrypted = InternalMessageParser.EncryptMessageWithBase64(str);
            var decrypted = InternalMessageParser.DecryptMessageWithBase64(encrypted);

            Assert.AreEqual(str, decrypted);
        }
    }
}