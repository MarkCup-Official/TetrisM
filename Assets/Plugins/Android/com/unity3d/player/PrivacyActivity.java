package com.unity3d.player;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.text.SpannableString;
import android.text.Spanned;
import android.text.method.LinkMovementMethod;
import android.text.style.ClickableSpan;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.TextView;

public class PrivacyActivity extends Activity implements DialogInterface.OnClickListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        // 如果已经同意过隐私协议则直接进入 Unity Activity
        if (GetPrivacyAccept()) {
            EnterUnityActivity();
            return;
        }
        
        ShowPrivacyDialog(); // 弹出隐私协议对话框
    }

    @Override
    public void onClick(DialogInterface dialogInterface, int i) {
        switch (i) {
            case AlertDialog.BUTTON_POSITIVE: // 点击同意按钮
                SetPrivacyAccept(true);
                EnterUnityActivity(); // 启动 Unity Activity
                break;
            case AlertDialog.BUTTON_NEGATIVE: // 点击拒绝按钮，直接退出 App
                finish();
                break;
        }
    }

    // 显示选项框
    private void ShowPrivacyDialog() {
        // 创建一个文字布局
        TextView textView = new TextView(this);
        textView.setText("Welcom to TetrisM!\nBefore you start, you need to carefully read and agree to our Privacy Policy.");
        textView.setPadding(64, 0, 64, 0);

        // 创建一个超链接文字布局
        TextView textView2 = new TextView(this);
        String privacyText = "TetrisM Privacy Policy";
        SpannableString spannableString = new SpannableString(privacyText);
        ClickableSpan clickableSpan = new ClickableSpan() {
            @Override
            public void onClick(View widget) {
                Uri uri = Uri.parse("https://docs.qq.com/pdf/DS09sUFliUEJRR2hH");//更换成你自己的用户协议文档的网址
                Intent intent = new Intent(Intent.ACTION_VIEW, uri);
                startActivity(intent);
            }
        };
        spannableString.setSpan(clickableSpan, 0, privacyText.length(), Spanned.SPAN_EXCLUSIVE_EXCLUSIVE);
        textView2.setText(spannableString);
        textView2.setMovementMethod(LinkMovementMethod.getInstance()); // 让超链接可点击
        textView2.setPadding(64, 0, 64, 0);

        // 创建一个布局
        LinearLayout layout = new LinearLayout(this);
        layout.setOrientation(LinearLayout.VERTICAL);
        layout.addView(textView);
        layout.addView(textView2);

        AlertDialog.Builder privacyDialog = new AlertDialog.Builder(this);
        privacyDialog.setCancelable(false);
        privacyDialog.setView(layout);
        privacyDialog.setTitle("TetrisM Privacy Policy");
        privacyDialog.setNegativeButton("Disagree", this);
        privacyDialog.setPositiveButton("Agree", this);
        privacyDialog.create().show();
    }

    // 启动 Unity Activity
    private void EnterUnityActivity() {
        Intent unityAct = new Intent();
        unityAct.setClassName(this, "com.unity3d.player.UnityPlayerActivity");
        startActivity(unityAct);
    }

    // 保存同意隐私协议状态
    private void SetPrivacyAccept(boolean accepted) {
        SharedPreferences.Editor prefs = this.getSharedPreferences("PlayerPrefs", MODE_PRIVATE).edit();
        prefs.putBoolean("PrivacyAccepted", accepted);
        prefs.apply();
    }

    // 检测是否已经同意过协议
    private boolean GetPrivacyAccept() {
        SharedPreferences prefs = this.getSharedPreferences("PlayerPrefs", MODE_PRIVATE);
        return prefs.getBoolean("PrivacyAccepted", false);
    }
}
