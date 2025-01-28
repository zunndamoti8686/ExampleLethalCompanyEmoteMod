using BepInEx;
using CustomEmotesAPI_Template_Mod;
using EmotesAPI;
using LethalEmotesAPI.ImportV2;
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
    public class ZunndaEmote /*right click on ExampleEmoteMod to the left and click rename, then type someting that matches your mod*/ : BaseUnityPlugin
    {
        public const string PluginGUID = "com.zunndamoti.ZunndaEmote"/*insert a string here to denote your plugin's GUID, such as: "com.author.modname"*/;
        public const string PluginName = "ZunndaEmote"/*insert a string here to denote your plugin's name, such as: "modname"*/;
        public const string PluginVersion = "1.0.0";
        public static PluginInfo PInfo { get; private set; }
        public static ZunndaEmote instance;


        public void Awake()
        {
            instance = this;
            PInfo = Info;
            CustomEmotesAPI_Template_Mod.Assets.LoadAssetBundlesFromFolder("assetbundles");

            CustomEmotesAPI.animChanged += CustomEmotesAPI_animChanged;
        }

        private void CustomEmotesAPI_animChanged(string newAnimation, BoneMapper mapper)
        {
            if (!newAnimation.StartsWith(PluginGUID))
            {
                return;
            }
            newAnimation = newAnimation.Split("__")[1];
        }

        //////////この関数は好きなだけ変更するか、まったく新しいものを作成することができます。
        public void ImportAnimation(AnimationClip[] primaryClips, AnimationClip[] secondaryClips, bool looping, AudioClip[] primaryAudioClips, bool sync, string customName, bool dmca, bool cantMove, bool thirdPerson)
        {
            CustomEmoteParams emoteParams = new CustomEmoteParams();
            emoteParams.primaryAnimationClips = primaryClips; //プライマリのアニメーションクリップのリスト、複数含めた場合はランダムに選ばれます
            emoteParams.secondaryAnimationClips = secondaryClips;//セカンダリのアニメーションクリップのリスト、primaryAnimationClipsと同じサイズでなければならず、同じスロットでランダムに選ばれます（例えば、primaryAnimationClips[3]が選ばれた場合、secondaryAnimation[3]も選ばれます）
            emoteParams.audioLoops = looping;//オーディオがループするかどうかを指定するために使用されます、単一のオーディオファイルをインポートする場合のみ設定する必要があります
            emoteParams.primaryAudioClips = primaryAudioClips;//プライマリのオーディオクリップのリスト
            emoteParams.secondaryAudioClips = null;//セカンダリのオーディオクリップのリスト、これらが指定されている場合、プライマリクリップはループせず、再生されるセカンダリクリップは常にループします
            emoteParams.primaryDMCAFreeAudioClips = null;//_primaryAudioClipsと同じですが、DMCA設定が許可する場合に再生されます（通常のオーディオクリップが存在し、dmcaクリップが存在しない場合、dmcaクリップは単に無音になります）
            emoteParams.secondaryDMCAFreeAudioClips = null;//_secondaryAudioClipsと同じですが、DMCA設定が許可する場合に再生されます
            emoteParams.visible = true;// falseの場合、すべての通常のエリアからエモートを非表示にしますが、PlayAnimationを通じて呼び出すことはできます。これはコード内でのみ必要なエモートに使用します
            emoteParams.syncAnim = sync;// trueの場合、すべてのエモートしている人々の間でアニメーションを同期します
            emoteParams.syncAudio = sync;// trueの場合、すべてのエモートしている人々の間でオーディオを同期します
            emoteParams.startPref = -1;// BoneMapperが他のインスタンスが再生されていないときに再生するprimaryAnimationClips配列のスポット、-1はランダム、-2は順次、それ以外は任意の設定
            emoteParams.joinPref = -1;// BoneMapperが少なくとも1つの他のインスタンスが再生されているときに再生するprimaryAnimationClips配列のスポット、-1はランダム、-2は順次、それ以外は任意の設定
            emoteParams.joinSpots = null;// アニメーションが再生されているときに表示される参加スポットの配列
            emoteParams.internalName = "";// エモートのカスタム内部名、指定しない場合、primaryAnimationClipsの最初のエモートが名前として使用されます
            emoteParams.lockType = AnimationClipParams.LockType.headBobbing;// エモートのロックタイプを決定します。none、headBobbing、lockHead、またはrootMotion
            emoteParams.willGetClaimedByDMCA = dmca;// 通常のオーディオセットがDMCAによってクレームされるかどうかをマークできます
            emoteParams.audioLevel = .3f;// エモートの音量を敵に警戒させるレベルで決定します。0は無音、1は最大
            emoteParams.rootBonesToIgnore = null; //指定されたボーンとその子ボーン（再帰的）はアニメーションされず、元のアニメーションを保持します（例：エモート中にプレイヤーが歩けるようにするために、2つの脚のルートボーンを無視することができます）
            emoteParams.soloBonesToIgnore = null; //上記と同じですが、指定されたボーンのみが無視されます
            emoteParams.stopWhenMove = cantMove;// オンの場合、プレイヤーが動き始めるとエモートをオフにします
            emoteParams.thirdPerson = thirdPerson;// trueの場合、デフォルトのアニメーションは三人称になります。
            emoteParams.displayName = customName;//指定された場合、エンドユーザーが目にする場所でエモートの名前を置き換えます。
            EmoteImporter.ImportEmote(emoteParams);
        }

        ImportAnimation([CustomEmotesAPI_Template_Mod.Assets.Load<AnimationClips>("YAJU&U_dance.anim")], null, true, [Assets.Load<AudioClips>("yaji&u_sabi.ogg")], true, "YAJU&U", false, true, true);
    }
}
