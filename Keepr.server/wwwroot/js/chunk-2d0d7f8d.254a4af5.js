(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-2d0d7f8d"],{"78a7":function(e,t,c){"use strict";c.r(t);var a=c("7a23");const n=Object(a["H"])("data-v-12967a24");Object(a["t"])("data-v-12967a24");const s={key:0},b=Object(a["g"])("h1",null,"Loading...",-1),o={class:"container-fluid"},d={class:"row"},j={class:"col"},O={class:"card-columns m-2"};Object(a["r"])();const r=n((e,t,c,n,r,l)=>{const i=Object(a["x"])("Keep");return Object(a["q"])(),Object(a["d"])(a["a"],null,[!0===n.state.loading?(Object(a["q"])(),Object(a["d"])("div",s,[b])):Object(a["e"])("",!0),Object(a["g"])("div",o,[Object(a["g"])("div",d,[Object(a["g"])("div",j,[Object(a["g"])("div",O,[(Object(a["q"])(!0),Object(a["d"])(a["a"],null,Object(a["w"])(n.state.keeps,e=>(Object(a["q"])(),Object(a["d"])(i,{key:e.id,"keep-prop":e},null,8,["keep-prop"]))),128))])])])])],64)});var l=c("83fc"),i=c("6c02"),u=c("6c96"),p={name:"Home",setup(){const e=Object(i["c"])(),t=Object(a["u"])({loading:!0,keeps:Object(a["b"])(()=>l["a"].keeps),user:Object(a["b"])(()=>l["a"].user),account:Object(a["b"])(()=>l["a"].account)});return Object(a["o"])(async()=>{try{await u["a"].getAllKeeps(),t.loading=!1}catch(e){Notification.toast("error:"+e,"danger")}}),{route:e,state:t}}};p.render=r,p.__scopeId="data-v-12967a24";t["default"]=p}}]);