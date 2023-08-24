using System;
public class Solution {
    public int solution(int n) {
        int answer = 0;
        
        double num = Math.Sqrt(n);
        
        for(int i=1;i<=num;++i)
        {
            if(n%i == 0)
            {
                int dividee = n/i;
                if(i != dividee)
                    answer += dividee;    
                answer += i;
            } 
        }
        
        return answer;
    }
}