using System;
public class Solution {
    public int solution(string s) {
        long answer = 0;
        int idx = 0;
        int length = 0;
        int digit = 0;
        bool isMinas = false;

        length = s.Length;
        digit = length-1;
          
        for(;idx<length;++idx)
        {
            if(!(s[idx] == '-' || s[idx] == '+'))
                answer += (long)(s[idx]-'0') * (long)Math.Pow(10,digit);
            digit-=1;
        }
        
        if(s[0] == '-')
            answer *= -1;
        
                
        
        // 너무 쉽자나 이건
        //answer = int.Parse(s);
        
        return (int)answer;
    }
}