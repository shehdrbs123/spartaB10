using System;
public class Solution {
    public int[] solution(long n) {
        int[] answer;
        string strNum = n.ToString();
        int strLength = strNum.Length;
        answer = new int[strLength];
        
        for(int i=0;i<strLength;++i)
        {
            answer[i] = strNum[strLength-1-i] - '0';
        }        
        
        return answer;
    }
}