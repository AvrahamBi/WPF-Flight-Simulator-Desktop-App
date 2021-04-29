Dll API
in case we want to add Dll the user need Dll that implement the folowwing methods and class:
/* 
* Classes
*/
/*
* stringWrapper
* for easy move strings from c# to c++
*/
class stringWrapper {
	std::string str;
public:
		stringWrapper(char* s2) {
		str = s2;
	}
	
	string get() return str;


/*
* the dll must contain this class! the reports should return with this data!
*/
 class AnomalyReport {
public:
	const string description;
	const long timeStep;
	AnomalyReport(string description, long timeStep) :description(description), timeStep(timeStep) {}
};
/*
* Method Name: CreatestringWrapper
* simply create an StringBuilder with the s2 string and return the string builder
*/
CreatestringWrapper(char* s2)

/*
* Method Name: createTimeSeries
* in this method the user create time series and return the timeSeries object
*/
void* createTimeSeries(stringWrapper * c, int s)
/*
* Method Name: Creates
* in this method the user need to create is decetor and learn the 
* normal flight state from the csv file at the end the user should return
* the Detector
*/
 void* Creates(TimeSeries * csv) 

 /*
 * Method name: anomalyData
 * in this method the dll need to return the anomalies as vector
 */
 void* anomalyData(SimpleAnomalyDetector * sad, TimeSeries * csv)

 /*
 *Method name: anomalyLength
 * return the number lengh of the anomaly reports 
 */
 double anomalyLength(vector<AnomalyReport>*v)
  /*
 *Method name: anomalyFirstElemnet/anomalyScondElemnet
 * if we give this method the vector of reports and place return the
 * first element name that the anomaly occurs (SecondElement return the second)
 */
  int anomalyFirstElemnet(vector<AnomalyReport>*v, int place)
   int anomalyScondElemnet(vector<AnomalyReport>*v, int place)

