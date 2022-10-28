using System.Collections.Generic;
using System;

using Eco.World;
using Eco.World.Blocks;
using Eco.Shared.Math;
using System.Diagnostics;
using Eco.Plugins.EcoLiveDataExporter.Poco;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class BlockCountUtils
    {

        private static Dictionary<String, int[]> everyBlock = new Dictionary<String, int[]>();

        private BlockCountUtils() {}

        public static readonly BlockCountUtils Instance = new BlockCountUtils();

        public async Task ScanWorld(){
            ClearBlocks();
            fillDictionary();
            await exportLiveBlockCountData();
        }

        /**
         * wake up every chunk in the world und iterate over there blocks
        **/
        private void fillDictionary(){
            var timer = Stopwatch.StartNew();
            Logger.Debug("Start scanning World");

            var chunks = Eco.World.World.Chunks;
            if(chunks == null) return;

            foreach (var oneChunk in chunks)
            {
                if(oneChunk == null)continue;
                oneChunk.WakeUp(new Vector3i(0,0,0));
                var arrBlock = oneChunk.Blocks;
                if(arrBlock == null)continue;

                foreach (var block in arrBlock)
                {
                    if(block == null) continue;
                    AddBlock(block);
                }
            }
            Logger.Debug($"scanning World in {timer.ElapsedMilliseconds} ms done");
        }

        public void AddBlock(Block block){
            if(block == null) return;
            String blockName = block.ToString();
            if(everyBlock.ContainsKey(blockName))
            {
                everyBlock[blockName][1]++;
            }
            else{
                everyBlock.Add(blockName, new int[] {block.GetID(), 1});
            }
        }
        public void RemoveBlock(Block block){
            if(block == null) return;
            String blockName = block.ToString();
            if(everyBlock.ContainsKey(blockName))
            {
                everyBlock[blockName][1]--;
            }
            else{
                Logger.Error("Error while removing Block from Block Count Dictonary");
            }
        }
        
        public void ClearBlocks(){
            everyBlock.Clear();
        }

        /**
         * iterate over the dictionary of all blocks to generate a json string
        **/
        private String getBlockCountString(){
            JsonBlockInfo jsonBlockInfo = new JsonBlockInfo();
            if(everyBlock == null){
                Logger.Debug("There are no Blocks to export!");
                return null;
            }
            try{
                foreach (KeyValuePair<string, int[]> item in everyBlock)
                {
                    jsonBlockInfo.AddBlockInfo(item);
                }
                var blockJson = JsonConvert.SerializeObject(jsonBlockInfo);
                return blockJson;
            }
            catch (Exception e)
            {
                Logger.Error($"Got an exception trying to export blockCount data: \n {e}");
                return null;
            }
        }

        /**
         * write every blocks to file
        **/
        private async Task exportLiveBlockCountData()
        {
            Logger.Debug("Exporting blockCount data");
            var BlockCountString = BlockCountUtils.Instance.getBlockCountString();
            if (BlockCountString == null || BlockCountString.Length == 0)
            {
                return;
            }
            await LocalFileExporter.WriteToFile("BlockCount", BlockCountString);
            Logger.Debug($"Finished exporting blockCount data");
        }
    }
}