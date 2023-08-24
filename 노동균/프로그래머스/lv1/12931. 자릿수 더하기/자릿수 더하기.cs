using System;

public class Solution {
    public int solution(int n) {
        int answer = 0;
        int num=0;
        int rest=0;
        
        while(n>0)
        {
            num = n%10;
            n /= 10;
            answer += num;
        }
        
        
        return answer;
    }
}