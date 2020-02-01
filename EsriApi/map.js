var spinner = document.querySelector(".spinnerContainer")
var MapURL = "https://sampleserver6.arcgisonline.com/arcgis/rest/services/Water_Network/FeatureServer/"
var map,featureLayer,graphicsLayer,LayerId;
var layerName = "Water System Valves - Block View"



require([
  "esri/request",
],
function(request) {
  request({
    url: MapURL,
    content: {f: "json"},
  }).then((res)=>{
    var LayerId ={}
    res.layers.forEach(l=> LayerId[l.name]=l.id)
    ///////////////////////////////////////////////////////////////////////
    
    require([
      "esri/map",
      "esri/layers/FeatureLayer",
      "esri/layers/GraphicsLayer"
    ],
function(Map,FeatureLayer,GraphicsLayer) {
  
  map = new Map("map", {
    basemap: "hybrid",
    center: [-82.44109, 35.6122],
    zoom: 17
  });
  featureLayer = new FeatureLayer
  (MapURL+LayerId[layerName], {
    "id":"featureLayer"
  });
  

  featureLayer.setMaxScale(map.getMaxScale())
  featureLayer.setMinScale(map.getMinScale())
  map.addLayer(featureLayer); 
  
  graphicsLayer = new GraphicsLayer({"id":"graphiclayer"})
  map.addLayer(graphicsLayer)
 $("#map > .spinnerContainer").hide()
 
 ExcuteQuery({
   url:MapURL+LayerId[layerName],
   where:"objectid<20000",
   outFields: ["facilityid", "valvetype", "diameter", "lastupdate"],
   callback:function(res){
    console.log(res)
    var orderData  = res.features.map(e=>{
      return{
        facilityid:e.attributes.facilityid,
        valvetype:e.attributes.valvetype,
        diameter:e.attributes.diameter, 
        lastupdate:new Date(e.attributes.lastupdate).toGMTString()
      }
    })
    var gridDataSource = new kendo.data.DataSource({
      data: orderData,
      schema: {
        model: {
          fields: {
            facilityid: { type: "number" },
            valvetype: { type: "string" },
            diameter: { type: "number" },
            lastupdate: { type: "string" },
          }
          }
      },
      sort: {
        field: "facilityid",
          dir: "desc"
        }
      });
      $("#Grid").kendoGrid({
        dataSource: gridDataSource,
        scrollable: true,
        sortable: true,
    searchable: true
  });
  
  $("#Grid > .k-grid-content > table > tbody > tr").hover(function(e){
    
    if(e.type=="mouseenter"){
      console.log(e.currentTarget.cells[0].innerText)
      console.log(res.features.find(function(k){return k.attributes.facilityid==e.currentTarget.cells[0].innerText}))
      var fea = res.features.find(function(k){return k.attributes.facilityid==e.currentTarget.cells[0].innerText}).geometry
      AddGraphic(map,graphicsLayer,fea)
      
      $("#GridDi > .k-grid-content > table > tbody > tr > td").filter(function() {
        return $(this)[0].innerHTML === e.currentTarget.cells[2].innerText;
      }).scrollintoview()
      
      $("#GridDi > .k-grid-content > table > tbody > tr > td").filter(function() { console.log($(this))
        return $(this)[0].innerHTML === e.currentTarget.cells[2].innerText;
      }).parent().css("background-color", "#e6f8f9");  
      
    }else{
      $("#GridDi > .k-grid-content > table > tbody > tr > td").parent().css("background-color", "");
      
    }
    
    
  })
  $("#Grid > .spinnerContainer").hide()
}
})


request({
  url: MapURL+LayerId[layerName],
    content: {f: "json"},
  })
  .then(function(result) {
        var orderData  = result.fields[5].domain.codedValues.map(e=>{
          return{
            name:e.name,
            code:e.code,
          }
        })
        var gridDataSource = new kendo.data.DataSource({
          data: orderData,
          schema: {
            model: {
              fields: {
                code: { type: "number" },
                name: { type: "string" }
              }
            }
          },
          columns: [
            { field: "code", title: "value" },
        ]
      });
      $("#GridDi").kendoGrid({
        dataSource: gridDataSource,
        scrollable: true
      });
      $("#GridDi > .spinnerContainer").hide()
      
    });
})


///////////////////////////////////////////////////////////////////////
})
})






//#region Healper Functions
////////////////////////////////////////////////////////////////
function AddGraphic(mapG,graphicL,feature){
  graphicL.clear()
  require([
    "esri/geometry/Point", "esri/symbols/SimpleMarkerSymbol",
    "esri/Color", "esri/graphic"
  ], function(Point, SimpleMarkerSymbol, Color, Graphic ) {
    debugger
      var sms = new SimpleMarkerSymbol().setStyle(
      SimpleMarkerSymbol.STYLE_SQUARE).setColor(
      new Color([255,0,0,0.5]));
      var graphic = new Graphic(feature,sms)
      console.log(graphic)
     graphicL.add(graphic)
     mapG.centerAt(feature)
     if(mapG.getZoom()>14){
       mapG.setZoom(14)
      }
  });
}
////////////////////////////////////////////////////////////////
function ExcuteQuery(options){
  require([ 
    "esri/tasks/query", 
    "esri/tasks/QueryTask",
    "esri/SpatialReference",
    "esri/request"
  ],
  function(Query, QueryTask,SpatialReference,request) {  
  var queryTask = new QueryTask(options.url);
  var query = new Query();
  query.where=options.where?options.where:"1=1";   //"objectid<20000"
  query.outSpatialReference = new SpatialReference(options.outSpatialReference?options.outSpatialReference:3857);
  query.outFields = options.outFields?options.outFields:["*"];
  query.returnGeometry = options.returnGeometry? options.returnGeometry:true ;
  queryTask.execute(query,options.callback)
})
}
////////////////////////////////////////////////////////////////
//#endregion