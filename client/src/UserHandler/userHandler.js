

class UserHandler {
   #User={
        Id:'',
        Name:'',
        Email:'',
        Password:'',
        BirthDay:undefined,
   };
   #Lroute="http://localhost:5126/api/users/"
   #Srout="http://localhost:5126/api/users/signup"
   constructor(email,password){
    this.#User.Email=email
    this.#User.Password=password
   }
   /**
    * TODO:Use Checker in Signup Method
    * @param {*email}  a string representing a user's email
    * @param {*password}   a string representing a user's password
    * @param {*} dateOfBirth a date specifying the birthday of a user
    * @returns {*Boolean} booleans that clarify if both passed regex Checks
    */
   #Checker(email,password,dateOfBirth)
   {
        const eReg=
        "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"
        let validateEFormat= eReg.test(email)
        const passRegex = "/^(?=.*\d)(?=.*[a-zA-Z]).{8,}$/";
        let validPassFormat= passRegex.test(password)
        // Calculate age based on dateOfBirth
        const today = new Date();
        const birthYear = dateOfBirth.getFullYear();
        const age = today.getFullYear() - birthYear;

        // Check if user is 16 or older
        const isOldEnough = age >= 16;

    return validateEFormat && validPassFormat && isOldEnough;
   }
    #HandleSignup(name,email,password,dateOfBirth){

    }
    #HandleLogin(email,password) {
        
    }
    Login(email,password) {
        let token='';
        return token
    }
    Signup(){
        
    }
}

export default UserHandler;