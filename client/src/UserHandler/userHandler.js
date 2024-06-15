import React from "react";

class UserHandler {
    static #User={
        Id:'',
        Name:'',
        Email:'',
        Password:'',
        Birthday:new Date(),
        BirthdayString:''
   };
   static Lroute="http://localhost:5126/api/users/"
   static Srout="http://localhost:5126/api/users/signup"
   /**
    * TODO:Use Checker in Signup Method
    * @param {*email}  a string representing a user's email
    * @param {*password}   a string representing a user's password
    * @param {*} dateOfBirth a date specifying the birthday of a user
    * @returns {*Boolean} booleans that clarify if both passed regex Checks
    */
   static getUserData() {
    return { ...UserHandler.#User }; // Spread operator to create a copy
  }
 
  
   static #Checker(email,password,dateOfBirth)
   {
        const eReg=
        new RegExp("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$")
        let validateEFormat= eReg.test(email)
        
        // eslint-disable-next-line no-useless-escape
        const passRegex = new RegExp("^(?=.*\\d)(?=.*[a-zA-Z]).{8,}$");
        let validPassFormat= passRegex.test(password)
        
        // Calculate age based on dateOfBirth
        const today = new Date();
        const birthYear = dateOfBirth.getFullYear();
        const age = today.getFullYear() - birthYear;

        // Check if user is 16 or older
        const isOldEnough = age >= 16;
       

    return validateEFormat && validPassFormat && isOldEnough;
   }
   
   static async Login(Email, Password) 
   {
        let token = '';
        let user = undefined;

        const options = {
            method: "POST",
            headers: {
            "Content-Type": "application/json"
            },
            body: JSON.stringify({ Email, Password }) // Correctly format the body
        };
        alert(`${Email}\n${Password}`)
        try {
            let res = await fetch(UserHandler.Lroute, options);

            if (!res.ok) {
                const errorData = await res.json();
                alert(errorData.Message || "An error occurred during login. #1");
                return;
            }   

        // Extract token from headers
            token = res.headers.get('Authorization')?.split(' ')[1];
            localStorage.setItem('token', token);

            // Extract user data from body
            const responseData = await res.json();
            user = responseData.User;

            // Populate UserHandler's user data
            UserHandler.#User = user;

            return token;
        } 
        catch (error) 
        {
            console.error('Error during login:', error);
            alert("An error occurred while logging in #2.");
        }
}


    static async Signup(Name,Email,Password,BirthDay){
        const errMsg =
        `Check the follwing errors
        1.Email may not be in the correct format
        2.Password must be 8 charecters long and include numbers
        3. date of birth ahead of current time or user must be 16 and older`
        alert(`${Email}\n${Password}\n${BirthDay}`)
        // Convert date of birth to ISO string
        const BirthDayUTC = new Date(Date.UTC(BirthDay.getFullYear(), BirthDay.getMonth(), BirthDay.getDate()));
        const BirthdayString = BirthDayUTC.toISOString();
        this.#User.Birthday=BirthDay;
        let val=UserHandler.#Checker(Email,Password,BirthDay)
        
        let options={
            "method":"POST",
            "headers": {
                "Content-type": "application/json"
            },
            "body":JSON.stringify({Name,Email,Password,BirthdayString})
        }
        if(val)
        {
            let res = await fetch(UserHandler.Srout,options);
            if(res.ok)
            {
                alert(`User added successfully`)
                return res
            }
            else if(!res.ok)
            {
                    alert("an error occurred")
            }
            else
            {
                alert("User already exists")
                return undefined
            }
        }
        else{
            alert(errMsg)
            return ;
        }
        
    }
}

export default UserHandler;