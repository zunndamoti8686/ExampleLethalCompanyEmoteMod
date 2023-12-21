using BepInEx;
using CustomEmotesAPI_Template_Mod;
using EmotesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lethal_Company_CustomEmotesAPI_Template_Mod
{
    [BepInDependency("com.weliveinasociety.CustomEmotesAPI")]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class ExampleEmoteMod : BaseUnityPlugin
    {
        public const string PluginGUID = "com.weliveinasociety.testmod";
        public const string PluginName = "testmod";
        public const string PluginVersion = "1.0.0";
        public static PluginInfo PInfo { get; private set; }
        public static ExampleEmoteMod instance;


        public void Awake()
        {
            instance = this;
            PInfo = Info;
            CustomEmotesAPI_Template_Mod.Assets.LoadAssetBundlesFromFolder("assetbundles");


            AnimationClipParams emoteParams = new AnimationClipParams();
            emoteParams.animationClip = [CustomEmotesAPI_Template_Mod.Assets.Load<AnimationClip>("assets/punch.anim")]; //list of primary animation clips, one of these will be picked randomly if you include more than one
            emoteParams.secondaryAnimation = null;// list of secondary animation clips, must be the same size as animationClip, will be picked randomly in the same slot (so if we pick animationClip[3] for an animation, we will also pick secondaryAnimation[3])
            emoteParams.looping = false;//used to specify if audio is looping, you only need to set this if you are importing a single audio file
            emoteParams._primaryAudioClips = null;//primary list of audio clips
            emoteParams._secondaryAudioClips = null;//secondary list of audio clips, if these are specified, the primary clip will never loop and the secondary clip that plays will always loop
            emoteParams._primaryDMCAFreeAudioClips = null;//same as _primaryAudioClips but will be played if DMCA settings allow it (if normal audio clips exist, and dmca clips do not, the dmca clips will simply be silence)
            emoteParams._secondaryDMCAFreeAudioClips = null;//same as _secondaryAudioClips but will be played if DMCA settings allow it
            emoteParams.visible = true;// If false, will hide the emote from all normal areas, however it can still be invoked through PlayAnimation, use this for emotes that are only needed in code
            emoteParams.syncAnim = false;// If true, will sync animation between all people emoting
            emoteParams.syncAudio = false;// If true, will sync audio between all people emoting
            emoteParams.startPref = -1;// Spot in animationClip array where a BoneMapper will play when there is no other instance of said emote playing -1 is random, -2 is sequential, anything else is what you make it to be
            emoteParams.joinPref = -1;// Spot in animationClip array where a BoneMapper will play when there is at least one other instance of said emote playing, -1 is random, -2 is sequential, anything else is what you make it to be
            emoteParams.joinSpots = null;// Array of join spots which will appear when the animation is playing
            emoteParams.customName = "My Cool Punch";// Custom name for emote, if not specified, the first emote from animationClip will be used as the name
            emoteParams.lockType = AnimationClipParams.LockType.headBobbing;// determines the lock type of your emote, none, headBobbing, lockHead, or rootMotion
            emoteParams.willGetClaimedByDMCA = false;// Lets you mark if your normal set of audio will get claimed by DMCA
            emoteParams.audioLevel = 0;// determines the volume of the emote in terms of alerting enemies, 0 is nothing, 1 is max

            CustomEmotesAPI.AddCustomAnimation(emoteParams);

            CustomEmotesAPI.animChanged += CustomEmotesAPI_animChanged;
        }

        private void CustomEmotesAPI_animChanged(string newAnimation, BoneMapper mapper)
        {
            if (newAnimation == "punch")
            {
                DebugClass.Log($"we punched");
            }
        }
    }
}
