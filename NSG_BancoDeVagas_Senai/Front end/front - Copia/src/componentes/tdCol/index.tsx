import React, {TdHTMLAttributes} from 'react';

interface TdProps extends TdHTMLAttributes<HTMLTableDataCellElement>{
    colSpan: number;
}

const td:React.FC<TdProps>=({colSpan})=>{
    return(
        <div>
            <td colSpan={colSpan}></td>
        </div>
    )
}

export default td;