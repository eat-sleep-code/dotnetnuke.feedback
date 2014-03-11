function phone_number_check(e)
{
	var character_code = (e.which) ? e.which : e.keyCode
//	// ALLOW ONLY NUMBERS
//	if (character_code > 31 && (character_code < 48 || character_code > 57))
//	{
//		return false;
//	}
	
	// ALLOW ONLY NUMBERS AND +
	//if (character_code > 31 && (character_code < 48 || character_code > 57) && character_code != 43) {
	//	return false;
	//}
	
	// ALLOW NUMBERS, SPACE KEY, +, -, (, )
	if (character_code > 32 && (character_code < 40 || character_code > 57) && character_code != 43 && character_code != 45 && character_code != 40 && character_code != 41) {
		return false;
	}
	
	return true;
}

function zip_code_check_us(e) 
{
	var character_code = (e.which) ? e.which : e.keyCode
	//ALLOW ONLY NUMBERS AND -
	if (character_code > 31 && (character_code < 48 || character_code > 57) && character_code != 45)
	{
		return false;
	}
	return true;
}

function zip_code_check_us_format(field) 
{
	var hyphencount = 0;
	if (field.length != 5 && field.length != 10) 
	{
		//alert("Invalid Zip Code");
		return false;
	}
	for (var i = 0; i < field.length; i++) 
	{
		temp = "" + field.substring(i, i+1);
		if (temp == "-") hyphencount++;
		if ((hyphencount > 1) || ((field.length == 10) && "" + field.charAt(5) != "-")) 
		{
			//alert("Invalid Zip Code");
			return false;
		}
	}
	return true;
}


