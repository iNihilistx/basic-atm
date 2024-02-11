class BankAccount:
    def __init__(self, AccountNumber, AccountPin, AccountBalance):
        self.number = AccountNumber
        self.pin = AccountPin
        self.balance = AccountBalance

    def withdraw_funds(self, amount):
        if amount <= self.balance:
            self.balance -= amount
            return True
        else:
            print("Insufficent Funds in Account...")
            return False
        
    def deposit_funds(self, amount):
        self.balance += amount


class Atm:
    def __init__(self):
        self.accounts = {}

    """
    method will add new account into the atm dictionary above
    it will take an account number, pin and the initial balance as parameters
    """
    def add_account(self, account_number, account_pin, account_balance):
        account = BankAccount(account_number, account_pin, account_balance) # this will then create a new object of BankAccount
        self.accounts[account_number] = account  # and then add the new account to the dictionary

    """
    method will authenticate the user by checking the account number and pin
    it will take the account number and pin as its parameters
    """
    def authenticate_user(self, account_number, account_pin):
        if account_number in self.accounts: # check if the account number exists in the dictionary
            if self.accounts[account_number].pin == account_pin: # checking if the pin matches the stores pin
                return True # it will then return true if its a match
            else:
                print("Pin is incorrect!")
                return False
        else:
            print("Account number is invalid!")
            return False
        
    def withdraw_funds(self, account_number, amount):
        if account_number in self.accounts:
            if self.accounts[account_number].withdraw_funds(amount):
                print("Funds Withdrawn!")
            else:
                print("Insufficent Funds in Account!")
        else:
            print("Account could not be found...")

    def deposit_funds(self, account_number, amount):
        if account_number in self.accounts:
            self.accounts[account_number].deposit_funds(amount)
            print("Funds Added Successfully!")
        else:
            print("Account could not be found...")


    def check_balance(self, account_number):
        if account_number in self.accounts:
            balance = self.accounts[account_number].balance
            print(f"Current Balance for: {account_number} is: £{balance}")
        else:
            print("Account could not be found...")




if __name__ == "__main__":
    atm = Atm()
    atm.add_account("123456789", "1234", 1000)

    account_number = input("Enter Account Number: ")
    account_pin = input("Enter Account Pin: ")

    if atm.authenticate_user(account_number, account_pin):
        print("Welcome to the ATM!")

        while True:
            print("1. Check Balance")
            print("2. Withdraw Funds")
            print("3. Deposit Funds")
            choice = int(input("Please select an option listed above: "))

            if choice == 1:
                atm.check_balance(account_number)
            elif choice == 2:
                withdraw_amount = int(input("Enter amount to withdraw: "))
                atm.withdraw_funds(account_number, withdraw_amount)
                atm.check_balance(account_number)
            elif choice == 3:
                deposit_amount = int(input("Enter amount to deposit: "))
                atm.deposit_funds(account_number, deposit_amount)
                atm.check_balance(account_number)
            else:
                print("Invalid key...")


    else:
        print("Incorrect Pin or Account Number...")