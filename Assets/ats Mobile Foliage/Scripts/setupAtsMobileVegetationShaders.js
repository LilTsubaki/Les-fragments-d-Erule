// These are only needed in case you are using the terrain engine version of the shader
var VertexLitTranslucencyColor : Color = Color(0.73,0.85,0.4,1);
var ShadowStrength = 0.8;


// wind
var Wind : Vector4 = Vector4(0.85,0.075,0.4,0.5);
var WindFrequency = 0.75;
var GrassWindFrequency = 1.5;

var TreeDistance = 500;
var MediumDetailsDistance = 5;


function Start ()
{
	
	//Application.targetFrameRate = 100;
	
	Shader.SetGlobalColor("_Wind", Wind);
	Shader.SetGlobalColor("_GrassWind", Wind);
	
    // Set up layer 9 to cull at our detail distance.
    var distances = new float[32];
    distances[8] = TreeDistance; // small things like DetailDistance of the terrain engine
    distances[9] = MediumDetailsDistance; // small things like DetailDistance of the terrain engine
    GetComponent.<Camera>().main.layerCullDistances = distances;
    
    // These variables are only needed in case you are using the terrain engine version of the shader
	Shader.SetGlobalColor("_TranslucencyColor", VertexLitTranslucencyColor);
	Shader.SetGlobalFloat("_TranslucencyViewDependency;", 0.65);
	//Shader.SetGlobalFloat("_Shininess;", VertexLitShininess);
	Shader.SetGlobalFloat("_ShadowStrength;", ShadowStrength);
	Shader.SetGlobalFloat("_ShadowOffsetScale;", 1.0);	
}


function FixedUpdate () {
	// simple wind animation
	// var WindRGBA : Color = Wind *  ( (Mathf.Sin(Time.realtimeSinceStartup * WindFrequency) + Mathf.Sin(Time.realtimeSinceStartup * WindFrequency * 0.975) )   * 0.5 );
	
	var WindRGBA : Color = Wind *  ( (Mathf.Sin(Time.realtimeSinceStartup * WindFrequency)));
	WindRGBA.a = Wind.w;
	var GrassWindRGBA : Color = Wind *  ( (Mathf.Sin(Time.realtimeSinceStartup * GrassWindFrequency)));
	GrassWindRGBA.a = Wind.w;
	
	//
	Shader.SetGlobalColor("_Wind", WindRGBA);
	Shader.SetGlobalColor("_GrassWind", GrassWindRGBA);
	
	//Shader.SetGlobalFloat("_Shininess;", VertexLitShininess);
}