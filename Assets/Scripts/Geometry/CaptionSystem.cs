using System.Collections.Generic;

namespace Geometry {
    public class CaptionSystem {
        
        private Dictionary<string, bool> captions =  new Dictionary<string, bool>();
        
        public CaptionSystem(List<string> captions) {
            foreach (string caption in captions) {
                this.captions[caption] = true;
            }
        }
        
        public string GetFreeCaption() {
            foreach (string caption in captions.Keys) {
                if (captions[caption]) {
                    captions[caption] = false;
                    return caption;
                }
            }
            throw new System.Exception("No captions found");
        }

        public void FreeCaption(string caption) {
            captions[caption] = false;
        }
    }
}