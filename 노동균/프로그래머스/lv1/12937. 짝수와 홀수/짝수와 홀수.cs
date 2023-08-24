using System;
public class Solution {
    public string solution(int num) {
        string answer = "Even";
        
        int rest = Math.Abs(num%2);
        if(rest == 1)
            answer = "Odd";
            
        return answer;
    }
}