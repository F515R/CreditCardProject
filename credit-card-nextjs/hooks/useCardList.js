import { useEffect, useState } from "react"
import { callApi } from "./apiMethods"



const useCardList =()=>{
    const [loading,setLoading] = useState(true)
    const [data,setData] = useState([])
    const [reaload,setReload] = useState({})
    const [errors,setErrors] = useState([])


    function forceReload(){ setReload({})}

    useEffect(() => {
        callApi('CreditCards').then(result =>
        {
            console.log(result)
            if(result.data!=null){
                setData(result.data)
            }else{
                setData([])
                setErrors(result.errors)
            }
            setLoading(false)
        })
    },[reaload])

    return [loading,data,errors,forceReload]
}

export default useCardList;